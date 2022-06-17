using PetriNetEngine.Domain.Services;
using SEA_Models.PetriNet;

namespace PetriNetEngine.Application;

public class SimulateTimedPetriNetService
{
    private readonly IPetriNetRepository _repository;
    
    public SimulateTimedPetriNetService(IPetriNetRepository petriNetRepository)
    {
        _repository = petriNetRepository;
    }
    public Place getPlaceFromId(PetriNet petriNet, int placeId)
    {
        return petriNet.Places.Find(p => p.PlaceId == placeId);
    }

    public List<int> GetPendingTransitions(int petriNetId)
    {
        // Check if petri net exists
        var petriNet = _repository.GetPetriNet(petriNetId);
        if (petriNet == null)
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }

        List<int> pendingTransitionIds = new List<int>();
        
        petriNet.Transitions.ForEach(t =>
        {
            //Get all input places with incoming normal arcs since only these can make a transition pending
            var inputPlacesIds =
                petriNet.Arcs.Where(a => a.TargetNode == t.TransitionId)
                    .Select(a => a.SourceNode).ToList();

            inputPlacesIds.ForEach(p =>
            {
                var place = getPlaceFromId(petriNet, p);
                
                place.Tokens.ForEach(token =>
                {
                    if (token.Age != null)
                    {
                        pendingTransitionIds.Add(t.TransitionId);
                    }
                });
            });
        });
        return pendingTransitionIds;
    }

    public List<int> GetUrgentPlaceIds(int petriNetId)
    {
        // Check if petri net exists
        var petriNet = _repository.GetPetriNet(petriNetId);
        if (petriNet == null)
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }

        List<int> urgenPlaceIds = new List<int>();
        petriNet.Places.ForEach(p =>
        {
            if (p.isUrgent == true)
            {
                urgenPlaceIds.Add(p.PlaceId);
            }
        });
        return urgenPlaceIds;
    }

    public List<int> GetEnabledTransitions(int petriNetId)
    {
        // Check if petri net exists
        var petriNet = _repository.GetPetriNet(petriNetId);
        if (petriNet == null)
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }
        
        var normalEnabledTransitionIds = new List<int>();
        var disabledTransitionIds = new List<int>();
        var inhibitorEnabledTransitionIds = new List<int>();

        petriNet.Transitions.ForEach(t =>
        {
            //Get all input places with incoming normal arcs
            var normalInputPlacesIds =
                petriNet.Arcs.Where(a => a.TargetNode == t.TransitionId && a.Type == ArcType.Normal)
                    .Select(a => a.SourceNode).ToList();

            //Get all input places with incoming inhibitor arcs
            var inhibitorInputPlacesIds =
                petriNet.Arcs.Where(a => a.TargetNode == t.TransitionId && a.Type == ArcType.Inhibitor)
                    .Select(a => a.SourceNode).ToList();
            
            
            // Check if transition should be enabled according to normal input places
            normalInputPlacesIds.ForEach(placeId =>
            {
                var place = getPlaceFromId(petriNet, placeId);
                // Enable all transitions, where places have no tokens
                if (place.Tokens.Count >= 1)
                {
                    normalEnabledTransitionIds.Add(t.TransitionId);
                }
                else
                {
                    disabledTransitionIds.Add(t.TransitionId);
                }
            });
        });
        
        // Enabled transitions are the union of all enabled transitions except from the disabled transitions
        List<int> union = normalEnabledTransitionIds.Concat(inhibitorEnabledTransitionIds).ToList();
        return union.Except(disabledTransitionIds).ToList();
    }

    public void AdvanceTime(int petriNetId)
    {
        // Check if petri net exists
        var petriNet = _repository.GetPetriNet(petriNetId);
        if (petriNet == null)
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }
        petriNet.Time += 1;
        petriNet.Places.ForEach(p =>
        {
            p.Tokens.ForEach(t =>
            {
                if (t.Age != null)
                {
                    t.Age += 1;
                    if (t.Age > p.maxAge)
                    {
                        p.isUrgent = true;
                    }
                }
            });
        });
        
        // Checks if it is possible to update the petri net?
        if (_repository.UpdatePetriNet(petriNetId, petriNet) == null)
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }
    }

    public void FireTransition(int petriNetId, int transitionId, string stringTokenIds)
    {
        // Get all tokenIds from https request "-1" means 0 arguments, otherwise ids of form "1,5,10"
        List<int> tokenIds = stringTokenIds.Split(',').Select(int.Parse).ToList();

        // Check if petri net exists
        var petriNet = _repository.GetPetriNet(petriNetId);
        if (petriNet == null)
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }
        
        // Check if the firing transition exists
        if (!petriNet.Transitions.Exists(t => t.TransitionId == transitionId))
        {
            throw new BadHttpRequestException("Invalid transition id");
        }
        
        // Check if the transition is enabled
        List<int> enableTransitions = GetEnabledTransitions(petriNetId);
        if (!enableTransitions.Contains(transitionId))
        {
            throw new BadHttpRequestException("Transition is not enabled");
        }
        
        // Checks if all token id's set requested to fire is unique
        if (tokenIds.Distinct().Count() != tokenIds.Count)
        {
            throw new BadHttpRequestException("All token id's are not unique");
        }
        
        // Find all input and output places for a transition
        var outputPlacesIds = new List<int>();
        var inputPlacesIds = new List<int>();
        
        bool allIncomingArcsAreInhibitor = true;
        petriNet.Arcs.ForEach(a =>
        {
            if (a.TargetNode == transitionId)
            {   
                inputPlacesIds.Add(a.SourceNode);
                if (a.Type == ArcType.Normal)
                {
                    allIncomingArcsAreInhibitor = false;
                }
            }
            if (a.SourceNode == transitionId)
            {
                outputPlacesIds.Add(a.TargetNode);
            }
        });

        // if http request contains only -1 as token id, there are no token ids given
        // and all incoming arcs must be inhibitor arcs.
        if (tokenIds[0] == -1)
        {
            // Initialize empty token list to consume.
            tokenIds = new List<int>();
            if (!allIncomingArcsAreInhibitor)
            {
                throw new BadHttpRequestException("Token Ids are invalid.");
            }
        }
        
        // Find tokens to be removed according to the http request
        
        List<Token> removedTokens = new List<Token>();
        foreach (int placeId in inputPlacesIds)
        {
            var place = getPlaceFromId(petriNet, placeId);
            foreach (var tokenId in tokenIds)
            {
                var t = place.Tokens.Find(t => t.TokenId == tokenId);
                if (t != null)
                {
                    // Break since we only want to remove 1 token from each input place
                    removedTokens.Add(t);
                    break;
                }
            }
        }

        // Checks there are more tokens in the http request than removed tokens
        if (removedTokens.Count != tokenIds.Count)
        {
            throw new BadHttpRequestException("Token id's are invalid." +
                                              " There should be one tokenId for each ingoing place");
        }

        // Removes the token from the database
        
        petriNet.Places.ForEach(p =>
        {
            removedTokens.ForEach(t =>
            {
                p.Tokens.Remove(t);
            });
            
            // Check if still urgent after removal
            if (p.isUrgent == true)
            {
                p.isUrgent = p.Tokens.Count(t => t.Age > p.maxAge) > 0;
            }
        });
        
        // add tokens to output places in the database
        petriNet.Places.ForEach(p =>
        {
            if (outputPlacesIds.Contains(p.PlaceId))
            {
                Token t = new Token
                {
                    TokenId = petriNet.MaxTokenId+1 ?? 0,
                    Age = 0
                };
                p.Tokens?.Add(t);
                petriNet.MaxTokenId += 1;
            }
        });

        // Checks if it is possible to update the petri net?
        if (_repository.UpdatePetriNet(petriNetId, petriNet) == null)
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }
    }
}
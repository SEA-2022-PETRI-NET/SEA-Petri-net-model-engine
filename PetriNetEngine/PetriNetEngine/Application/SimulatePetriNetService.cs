using SEA_Models.Model;
using PetriNetEngine.Domain.Services;

namespace PetriNetEngine.Application;

public class SimulatePetriNetService
{
    private readonly IPetriNetRepository _repository;
    
    public SimulatePetriNetService(IPetriNetRepository petriNetRepository)
    {
        _repository = petriNetRepository;
    }

    public List<int> GetEnabledTransitions(int petriNetId)
    {
        var petriNet = _repository.GetPetriNet(petriNetId);
        if (petriNet == null)
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }
        
        var enabledTransitionIds = new List<int>();
        petriNet.Transitions.ForEach(t =>
        {
            var inputPlacesIds =
                petriNet.Arcs.Where(a => a.TargetNode == t.TransitionId).Select(a => a.SourceNode).ToList();
            var inputPlaces = petriNet.Places.Where(p => inputPlacesIds
                .Exists(p1 => p1 == p.PlaceId)).ToList();
            
            if (inputPlaces.All(p => p.NumberOfTokens >= 1))
            {
                enabledTransitionIds.Add(t.TransitionId);
            }
        });
        return enabledTransitionIds;
    }

    public void FireTransition(int petriNetId, int transitionId)
    {
        var petriNet = _repository.GetPetriNet(petriNetId);
        if (petriNet == null)
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }

        if (!petriNet.Transitions.Exists(t => t.TransitionId == transitionId))
        {
            throw new BadHttpRequestException("Invalid transition id");
        }

        var outputPlacesIds = new List<int>();
        var inputPlacesIds = new List<int>();
        petriNet.Arcs.ForEach(a =>
        {
            if (a.TargetNode == transitionId)
            {
                inputPlacesIds.Add(a.SourceNode);
            }

            if (a.SourceNode == transitionId)
            {
                outputPlacesIds.Add(a.TargetNode);
            }
        });

        petriNet.Places.ForEach(p =>
        {
            if (inputPlacesIds.Contains(p.PlaceId) && (p.NumberOfTokens == null || p.NumberOfTokens < 1))
            {
                throw new BadHttpRequestException("Transition is not enabled");
            }

            if (inputPlacesIds.Contains(p.PlaceId) && p.NumberOfTokens != null)
            {
                p.NumberOfTokens = p.NumberOfTokens.Value - 1;
            }

            if (outputPlacesIds.Contains(p.PlaceId))
            {
                p.NumberOfTokens = p.NumberOfTokens == null ? 1 : p.NumberOfTokens.Value + 1;
            }
        });

        if (!_repository.UpdatePetriNet(petriNet))
        {
            throw new BadHttpRequestException("Invalid petri net id");
        }
    }


}
using SEA_Models.PetriNet;

namespace PetriNetEngine.Application;

public class ValidatePetriNetService : IValidatePetriNetService
{
    public void Validate(PetriNet petriNet)
    {
        var nodeNames = petriNet.Places!.Select(p => p.Name).ToList();
        nodeNames.AddRange(petriNet.Transitions!.Select(t => t.Name));
        if (nodeNames.Distinct().Count(n => !String.IsNullOrEmpty(n)) != nodeNames.Count(n => !String.IsNullOrEmpty(n)))
        {
            throw new BadHttpRequestException("Transition and places names should all be unique");
        }

        var placeIds = petriNet.Places!.Select(p => p.PlaceId).ToList();
        var transitionIds = petriNet.Transitions!.Select(t => t.TransitionId).ToList();
        var nodeIds = placeIds.Concat(transitionIds).ToList();
        if (nodeIds.Distinct().Count() != nodeIds.Count)
        {
            throw new BadHttpRequestException($"Transition and places ids within a single petri net should all be unique. " +
                                              $"\n Node ids: {String.Join("; ", nodeIds)}");
        }
        
        petriNet.Arcs!.ForEach(a =>
        {
            if ( !((placeIds.Contains(a.SourceNode) || transitionIds.Contains(a.SourceNode)) && 
                 (placeIds.Contains(a.TargetNode) || transitionIds.Contains(a.TargetNode))))
            {
                throw new BadHttpRequestException(
                    $"The source nodes and target nodes of arcs must be either existing places or transitions. ");
            }
            
            if ((placeIds.Exists(p => p == a.SourceNode) && placeIds.Exists(t => t == a.TargetNode)) ||
                (transitionIds.Exists(p => p == a.SourceNode) && transitionIds.Exists(t => t == a.TargetNode)))
            {
                throw new BadHttpRequestException("An arc cannot go from a place to another place or from a transition to another transition.");
            }
        });
    }
}
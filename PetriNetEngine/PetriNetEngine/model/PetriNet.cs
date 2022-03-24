namespace PetriNetEngine.model;

public class PetriNet
{
    public Arc[] Arcs { get; set; }
    public Place[] Places { get; set; }
    public Transition[] Transitions { get; set; }

    public PetriNet(Arc[] arcs, Place[] places, Transition[] transitions)
    {
        Arcs = arcs;
        Places = places;
        Transitions = transitions;
    }
    
}
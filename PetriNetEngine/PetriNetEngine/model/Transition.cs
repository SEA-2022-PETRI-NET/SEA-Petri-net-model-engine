namespace PetriNetEngine.model;

public class Transition : INode
{
    public Int32 Id { get; set; }

    public Transition(Int32 id)
    {
        Id = id;
    }
}
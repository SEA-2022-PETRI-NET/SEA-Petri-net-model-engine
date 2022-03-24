namespace PetriNetEngine.model;

public class Place : INode
{
    public Int32 Id { get; set; }

    public Place(Int32 id)
    {
        Id = id;
    }
}
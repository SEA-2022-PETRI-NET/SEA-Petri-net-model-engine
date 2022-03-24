namespace PetriNetEngine.model;

public class Arc
{
    public Int32 SourceNode { get; set; }
    
    public Int32 TargetNode { get; set; }

    public Arc(Int32 sourceNode, Int32 targetNode)
    {
        SourceNode = sourceNode;
        TargetNode = targetNode;
    }
}
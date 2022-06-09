namespace SEA_Models.PetriNet;

public class PetriNet
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public int? Time { get; set; }
    public int? MaxTokenId { get; set; }
    public List<Arc>? Arcs { get; set; }
    public List<Place>? Places { get; set; }
    public List<Transition>? Transitions { get; set; }
}
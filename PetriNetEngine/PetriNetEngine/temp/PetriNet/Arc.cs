using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SEA_Models.PetriNet;

public class Arc
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public int SourceNode { get; set; }
    
    public int TargetNode { get; set; }
    
    public ArcType Type { get; set; }
        
    [JsonIgnore]
    public int? PetriNetId { get; set; }
    [JsonIgnore]
    public PetriNet? PetriNet { get; set; }
}
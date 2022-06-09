using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEA_Models.PetriNet;

public class Place
{
    public int Id { get; set; }
    
    public int PlaceId { get; set; }
    public string? Name { get; set; }
    
    
    [JsonIgnore]
    public int? PetriNetId { get; set; }
    [JsonIgnore]
    public PetriNet? PetriNet { get; set; }
    
    public int? maxAge { get; set; }
    
    public bool? isUrgent { get; set; }

    // We should either you one or the other of these two fields.
    // This depends on if we are working with a coloured petri net or not. 
    [Range(0, int.MaxValue)]
    public int? NumberOfTokens { get; set; } 
    public List<Token>? Tokens { get; set; }
}
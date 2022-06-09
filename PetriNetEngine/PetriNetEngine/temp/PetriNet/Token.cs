using System.Text.Json.Serialization;

namespace SEA_Models.PetriNet;

public class Token
{
    public int Id { get; set; }
    
    public int TokenId { get; set; }
    public string? Name { get; set; }
    
    public int? Age { get; set; }
    
    [JsonIgnore]
    public int? PlaceId { get; set; }
    [JsonIgnore]
    public Place? Place { get; set; }
    
    [JsonIgnore]
    public int? PetriNetId { get; set; }
    [JsonIgnore]
    public PetriNet? PetriNet { get; set; }
}
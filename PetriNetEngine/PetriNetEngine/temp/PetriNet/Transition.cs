using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace SEA_Models.PetriNet;

public class Transition
{
    public int Id { get; set; }
    
    public int TransitionId { get; set; }
    
    public string? Name { get; set; }
    
    [JsonIgnore]
    public int? PetriNetId { get; set; }
    [JsonIgnore]
    public PetriNet? PetriNet { get; set; }
}
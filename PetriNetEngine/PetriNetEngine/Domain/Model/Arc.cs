using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace PetriNetEngine.Domain.Model;

public class Arc
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public int SourceNode { get; set; }
    
    public int TargetNode { get; set; }
    
    [JsonIgnore]
    public int? PetriNetId { get; set; }
    [JsonIgnore]
    public PetriNet? PetriNet { get; set; }
}
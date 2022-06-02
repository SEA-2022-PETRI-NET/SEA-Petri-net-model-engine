using Microsoft.EntityFrameworkCore;
using SEA_Models.Domain.Model.PetriNet;

namespace PetriNetEngine.Infrastructure;

public class PetriNetContext : DbContext
{
    public PetriNetContext(DbContextOptions<PetriNetContext> options) : base(options) { }
    public DbSet<PetriNet> PetriNets { get; set; } = null!;
    public DbSet<Arc> Arcs { get; set; } = null!;
    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<Transition> Transitions { get; set; } = null!;
}
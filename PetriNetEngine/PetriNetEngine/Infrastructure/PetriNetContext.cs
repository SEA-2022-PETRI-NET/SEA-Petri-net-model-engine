using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SEA_Models.PetriNet;

namespace PetriNetEngine.Infrastructure;

public class PetriNetContext : DbContext
{
    public PetriNetContext(DbContextOptions<PetriNetContext> options) : base(options) { }
    public DbSet<PetriNet> PetriNets { get; set; } = null!;
    public DbSet<Arc> Arcs { get; set; } = null!;
    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<Transition> Transitions { get; set; } = null!;
    public DbSet<Token> Tokens { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Place>().OwnsOne(p => p.position);
        modelBuilder.Entity<Transition>().OwnsOne(t => t.position);
    }
}
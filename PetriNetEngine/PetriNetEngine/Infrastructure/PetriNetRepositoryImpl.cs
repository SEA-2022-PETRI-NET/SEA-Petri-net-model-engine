using Microsoft.EntityFrameworkCore;
using PetriNetEngine.Domain.Services;
using SEA_Models.PetriNet;

namespace PetriNetEngine.Infrastructure;

public class PetriNetRepositoryImpl : IPetriNetRepository
{
    private readonly PetriNetContext _petriNetContext;
    
    public PetriNetRepositoryImpl(PetriNetContext petriNetContext){
        _petriNetContext = petriNetContext;
    }
    
    public List<PetriNet> GetPetriNets()
    {
        return _petriNetContext.PetriNets
            .Include(p => p.Places)!
                .ThenInclude(p => p.Tokens)
            .Include(p => p.Transitions)
            .Include(p => p.Arcs)
            .ToList();
    }

    public PetriNet? GetPetriNet(int id)
    {
        return _petriNetContext.PetriNets
            .Include(p => p.Places)!
                .ThenInclude(p => p.Tokens)
            .Include(p => p.Transitions)
            .Include(p => p.Arcs)
            .FirstOrDefault(p => p.Id == id);
    }

    public PetriNet Save(PetriNet petriNet)
    {
        var storedPetriNet = _petriNetContext.PetriNets.Add(petriNet);
        _petriNetContext.SaveChanges();
        return storedPetriNet.Entity;
    }

    public PetriNet? UpdatePetriNet(int id, PetriNet petriNet)
    {
        var storedPetriNet = GetPetriNet(id);
        if (storedPetriNet == null) return null;
        storedPetriNet.Name = petriNet.Name;
        storedPetriNet.Arcs = petriNet.Arcs;
        storedPetriNet.Transitions = petriNet.Transitions;
        storedPetriNet.Places = petriNet.Places;
        _petriNetContext.SaveChanges();
        return storedPetriNet;
    }

    public bool Delete(int id)
    {
        var petriNet = GetPetriNet(id);
        if (petriNet == null) return false;
        petriNet.Arcs.ForEach(a => _petriNetContext.Arcs.Remove(a));
        petriNet.Transitions.ForEach(t => _petriNetContext.Transitions.Remove(t));
        petriNet.Places.ForEach(p => _petriNetContext.Places.Remove(p));
        _petriNetContext.PetriNets.Remove(petriNet);
        _petriNetContext.SaveChanges();
        return true;
    }
}
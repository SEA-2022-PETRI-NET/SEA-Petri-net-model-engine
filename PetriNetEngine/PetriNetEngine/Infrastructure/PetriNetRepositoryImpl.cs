using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetriNetEngine.Domain.Services;
using SEA_Models.Model;

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
            .Include(p => p.Places)
            .Include(p => p.Transitions)
            .Include(p => p.Arcs)
            .ToList();
    }

    public PetriNet? GetPetriNet(int id)
    {
        return _petriNetContext.PetriNets
            .Include(p => p.Places)
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

    public bool UpdatePetriNet(PetriNet petriNet)
    {
        var storedPetriNet = _petriNetContext.PetriNets
            .Include(p => p.Places)
            .Include(p => p.Transitions)
            .Include(p => p.Arcs)
            .FirstOrDefault(p => p.Id == petriNet.Id);
        if (storedPetriNet == null) return false;
        storedPetriNet.Name = petriNet.Name;
        storedPetriNet.Arcs = petriNet.Arcs;
        storedPetriNet.Transitions = petriNet.Transitions;
        storedPetriNet.Places = petriNet.Places;
        _petriNetContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var petriNet = _petriNetContext.PetriNets.FirstOrDefault(p => p.Id == id);
        if (petriNet == null) return false;
        _petriNetContext.PetriNets.Remove(petriNet);
        _petriNetContext.SaveChanges();
        return true;
    }
}
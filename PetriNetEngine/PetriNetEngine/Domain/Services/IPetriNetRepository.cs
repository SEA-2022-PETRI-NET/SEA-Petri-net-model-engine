using PetriNetEngine.Domain.Model;

namespace PetriNetEngine.Domain.Services;

public interface IPetriNetRepository
{
    List<PetriNet> GetPetriNets();

    PetriNet? GetPetriNet(int id);
    
    int Save(PetriNet petriNet);

    void Delete(int id);

    public void UpdatePetriNet(PetriNet petriNet);
}
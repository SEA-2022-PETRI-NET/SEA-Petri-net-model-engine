using SEA_Models.Domain.Model.PetriNet;

namespace PetriNetEngine.Domain.Services;

public interface IPetriNetRepository
{
    List<PetriNet> GetPetriNets();

    PetriNet? GetPetriNet(int id);
    
    PetriNet Save(PetriNet petriNet);

    bool Delete(int id);

    bool UpdatePetriNet(PetriNet petriNet);
}
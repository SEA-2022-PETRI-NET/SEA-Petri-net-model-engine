using PetriNetEngine.Domain.Services;
using SEA_Models.PetriNet;

namespace PetriNetEngine.Application;

public interface IModelPetriNetService
{

    List<PetriNet> GetAll();

    PetriNet? GetPetriNet(int id);

    PetriNet CreateNetPetriNet(PetriNet petriNetDto);
    
    PetriNet? UpdatePetriNet(PetriNet petriNetDto);

    bool DeletePetriNet(int id);
}
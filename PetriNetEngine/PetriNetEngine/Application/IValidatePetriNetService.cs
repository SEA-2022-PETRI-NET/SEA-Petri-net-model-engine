using SEA_Models.PetriNet;

namespace PetriNetEngine.Application;

public interface IValidatePetriNetService
{
    void Validate(PetriNet petriNet);
}
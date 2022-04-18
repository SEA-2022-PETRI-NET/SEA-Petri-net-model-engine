using PetriNetEngine.Domain.Services;
using PetriNetEngine.Domain.Model;

namespace PetriNetEngine.Application;

public class ModelPetriNetService
{
    private readonly IPetriNetRepository _repository;
    private readonly ValidatePetriNetService _validatePetriNetService;
    
    public ModelPetriNetService(IPetriNetRepository petriNetRepository, ValidatePetriNetService validatePetriNetService)
    {
        _repository = petriNetRepository;
        _validatePetriNetService = validatePetriNetService;
    }

    public List<PetriNet> GetAll()
    {
        return _repository.GetPetriNets();
    }

    public PetriNet? GetPetriNet(int id)
    {
        return _repository.GetPetriNet(id);
    }

    public int CreateNetPetriNet(PetriNet petriNetDto)
    {
        PetriNet petriNet = new PetriNet
        {
            Name = petriNetDto.Name,
            Arcs = petriNetDto.Arcs,
            Places = petriNetDto.Places,
            Transitions = petriNetDto.Transitions,
        };
        _validatePetriNetService.validate(petriNet);
        return _repository.Save(petriNet);
    }

    public void DeletePetriNet(int id)
    {
        _repository.Delete(id);
    }
}
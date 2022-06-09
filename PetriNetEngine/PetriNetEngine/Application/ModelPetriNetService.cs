using PetriNetEngine.Domain.Services;
using SEA_Models.PetriNet;

namespace PetriNetEngine.Application;

public class ModelPetriNetService : IModelPetriNetService
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

    public PetriNet CreateNetPetriNet(PetriNet petriNetDto)
    {
        var petriNet = new PetriNet
        {
            Name = petriNetDto.Name,
            Arcs = petriNetDto.Arcs,
            Places = petriNetDto.Places,
            Transitions = petriNetDto.Transitions,
        };
        _validatePetriNetService.Validate(petriNet);
        return _repository.Save(petriNet);
    }

    public PetriNet? UpdatePetriNet(PetriNet petriNetDto)
    {
        var petriNet = new PetriNet
        {
            Name = petriNetDto.Name,
            Arcs = petriNetDto.Arcs,
            Places = petriNetDto.Places,
            Transitions = petriNetDto.Transitions,
        };
        _validatePetriNetService.Validate(petriNet);
        return _repository.UpdatePetriNet(petriNet);
    }

    public bool DeletePetriNet(int id)
    {
        return _repository.Delete(id);
    }
}
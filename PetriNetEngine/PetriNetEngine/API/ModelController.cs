using Microsoft.AspNetCore.Mvc;
using PetriNetEngine.Application;
using PetriNetEngine.Domain.Model;

namespace PetriNetEngine.API;

[ApiController]
[Route("api/v1/[controller]")]
public class ModelController : ControllerBase
{
    private readonly ModelPetriNetService _modelPetriNetService;

    public ModelController(ModelPetriNetService modelPetriNetService)
    {
        _modelPetriNetService = modelPetriNetService;
    }
    
    [HttpGet( Name = "GetAllPetriNets")]
    public List<PetriNet> GetAll()
    {
        return _modelPetriNetService.GetAll();
    }

    [HttpGet("{id}", Name = "GetPetriNet")]
    public PetriNet? Get(int id)
    {
        return _modelPetriNetService.GetPetriNet(id);
    }
    
    [HttpPost(Name = "CreatePetriNet")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public int Create(PetriNet petriNet)
    {
        return _modelPetriNetService.CreateNetPetriNet(petriNet);
    }
    
    [HttpDelete("{id}", Name = "DeletePetriNet")]
    public async void Delete(int id)
    {   
        _modelPetriNetService.DeletePetriNet(id);
    }
}
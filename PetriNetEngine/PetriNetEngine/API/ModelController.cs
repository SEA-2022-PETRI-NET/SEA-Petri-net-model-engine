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
    [ProducesResponseType(typeof(PetriNet), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        var petriNet = _modelPetriNetService.GetPetriNet(id);
        return petriNet == null ? NotFound() : Ok(petriNet);
    }
    
    [HttpPost(Name = "CreatePetriNet")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(PetriNet petriNet)
    {
        int petriNetId = _modelPetriNetService.CreateNetPetriNet(petriNet);
        return CreatedAtAction(nameof(Get), new { id = petriNetId }, petriNet);
    }
    
    [HttpDelete("{id}", Name = "DeletePetriNet")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        if (!_modelPetriNetService.DeletePetriNet(id))
        {
            return NotFound();
        }
        return NoContent();
    }
}
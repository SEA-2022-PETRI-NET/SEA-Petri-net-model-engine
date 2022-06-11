using Microsoft.AspNetCore.Mvc;
using PetriNetEngine.Application;
using SEA_Models.PetriNet;

namespace PetriNetEngine.API;

[ApiController]
[Route("api/v1/[controller]")]
public class ModelController : ControllerBase
{
    private readonly IModelPetriNetService _modelPetriNetService;
    private readonly IValidatePetriNetService _validatePetriNetService;

    public ModelController(IModelPetriNetService modelPetriNetService, 
        IValidatePetriNetService validatePetriNetService)
    {
        _modelPetriNetService = modelPetriNetService;
        _validatePetriNetService = validatePetriNetService;
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
    [ProducesResponseType(typeof(PetriNet), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(PetriNet petriNet)
    {
        var createdNet = _modelPetriNetService.CreateNetPetriNet(petriNet);
        return CreatedAtAction(nameof(Get), new { id = createdNet.Id }, createdNet);
    }
    
    [HttpPut("update-petri-net", Name = "UpdatePetriNet")]
    [ProducesResponseType(typeof(PetriNet), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(PetriNet petriNet)
    {
        var createdNet = _modelPetriNetService.UpdatePetriNet(petriNet);
        if (createdNet == null)
        {
            return NotFound();
        }
        return Ok(createdNet);
    }
    
    [HttpPost("validate-petri-net", Name = "ValidatePetriNet")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Validate(PetriNet petriNet)
    {
        _validatePetriNetService.Validate(petriNet);
        return Accepted();
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
using Microsoft.AspNetCore.Mvc;
using PetriNetEngine.Application;
using SEA_Models.PetriNet;

namespace PetriNetEngine.API;

[ApiController]
[Route("api/v1/[controller]")]
public class SimulationController : ControllerBase
{
    private readonly SimulatePetriNetService _simulatePetriNetService;

    public SimulationController(SimulatePetriNetService simulatePetriNetService)
    {
        _simulatePetriNetService = simulatePetriNetService;
    }
    
    [HttpGet( "{petriNetId}", Name = "GetEnabledTransitions")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<Transition>), StatusCodes.Status200OK)]
    public IActionResult GetEnabledTransitions(int petriNetId)
    {
        var transitions = _simulatePetriNetService.GetEnabledTransitions(petriNetId);
        return Ok(transitions);
    }
    
    [HttpPut( "{petriNetId}/{transitionId}", Name = "FireTransition")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult FireTransition(int petriNetId, int transitionId)
    {
        _simulatePetriNetService.FireTransition(petriNetId, transitionId);
        return Ok();
    }
}
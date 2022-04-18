using Microsoft.AspNetCore.Mvc;
using PetriNetEngine.Application;

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
    public List<int> GetEnabledTransitions(int petriNetId)
    {
        return _simulatePetriNetService.GetEnabledTransitions(petriNetId);
    }
    
    [HttpPost( "{petriNetId}/{transitionId}", Name = "FireTransition")]
    public void FireTransition(int petriNetId, int transitionId)
    {
        _simulatePetriNetService.FireTransition(petriNetId, transitionId);
    }
}
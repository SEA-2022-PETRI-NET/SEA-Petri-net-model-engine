using Microsoft.AspNetCore.Mvc;
using PetriNetEngine.model;

namespace PetriNetEngine.Controllers;

[ApiController]
[Route("[controller]")]
public class ModellingController : ControllerBase
{
    private readonly ILogger<ModellingController> _logger;

    public ModellingController(ILogger<ModellingController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetPetriNet")]
    public PetriNet Get()
    {   
        Arc[] arcs = new [] {new Arc(1, 2), new Arc(2, 1)};
        Place[] places = new[] {new Place(1)};
        Transition[] transitions = new[] {new Transition(2)};
        PetriNet net = new PetriNet(arcs, places, transitions);
        return net;
    }
}
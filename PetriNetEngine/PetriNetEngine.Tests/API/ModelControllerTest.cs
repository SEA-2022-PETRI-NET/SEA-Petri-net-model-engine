using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PetriNetEngine.API;
using Moq;
using PetriNetEngine.Application;
using SEA_Models.PetriNet;

namespace PetriNetEngine.Tests;

public class ModelControllerTest
{

    [Fact]
    public void GET_modelling()
    {
        //Given
        var mock = new Mock<IModelPetriNetService>();
        var mock2 = new Mock<IValidatePetriNetService>();
        ModelController mc = new ModelController(mock.Object, mock2.Object);
        //When
        var res = mc.GetAll();
        //Then
        mock.Verify(x=>x.GetAll(),Times.Once());

    }
}



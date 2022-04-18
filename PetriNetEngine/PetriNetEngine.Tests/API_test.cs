using Xunit;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
namespace PetriNetEngine.Tests;

public class API_test
{
    [Fact]
    public async Task GET_modelling()
    {


        await using var application = new WebApplicationFactory<Program>();

         var client = application.CreateClient();

        var response = await client.GetAsync("api/v1/Model");
        Assert.True(response.IsSuccessStatusCode);
    }
}



using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
namespace PetriNetEngine.Tests;

public class ModelPetriNetTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    public ModelPetriNetTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task GET_modelling()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        var response = await client.GetAsync("api/v1/Model");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Create_moddeling()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        var response = await client.PostAsync("api/v1/Model", System.Net.Http.HttpContent);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
    }
    
}


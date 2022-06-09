using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
namespace PetriNetEngine.Tests;

public class ApiTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    public ApiTest(ITestOutputHelper testOutputHelper)
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
        //Assert.True(response.IsSuccessStatusCode);
        
    }
}



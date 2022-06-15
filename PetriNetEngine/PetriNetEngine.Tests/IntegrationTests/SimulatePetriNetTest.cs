using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace PetriNetEngine.Tests.IntegrationTests;

public class SimulatePetriNetTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    public SimulatePetriNetTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task GET_Simulation()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        var response = await client.GetAsync("/api/v1/Simulation/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GET_Fire()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        
        StringContent httpContent = new StringContent("{    \"name\": \"P\",    \"time\": 0,    \"maxTokenId\": 0,    \"arcs\": [      {        \"sourceNode\": 0,        \"targetNode\": 1,        \"type\": 0      }    ],    \"places\": [      {        \"placeId\": 1,        \"name\": \"Place\",        \"maxAge\": 0,        \"isUrgent\": false,        \"numberOfTokens\": 2147483647,        \"tokens\": [          {            \"id\": 0,            \"tokenId\": 0,            \"name\": \"string\",            \"age\": 0          }        ]      }    ],    \"transitions\": [      {        \"transitionId\": 0,        \"name\": \"0\"      }    ]  }", System.Text.Encoding.UTF8, "application/json");

        var res = await client.PostAsync("api/v1/Model", httpContent);
        _testOutputHelper.WriteLine(res.ToString());
        _testOutputHelper.WriteLine(res.Headers.Location.ToString());
        string[] subs = res.Headers.Location.ToString().Split('/');
        List<string> list = new List<string>(subs);
        string last = list[^1];
        int id = Int32.Parse(last);
        _testOutputHelper.WriteLine(id.ToString());

        var response = await client.GetAsync("/api/v1/Simulation/" + id.ToString());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}

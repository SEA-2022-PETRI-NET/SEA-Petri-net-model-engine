using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using SEA_Models.PetriNet;

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
    public async Task Create_modeling()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        PetriNet p = new PetriNet();
        p.Places = new List<Place>()
        {
            new Place()
        };
        p.Transitions = new List<Transition>()
        {
            new Transition()
        };
        p.Arcs = new List<Arc>()
        {
            new Arc()
        };
        
        StringContent httpContent = new StringContent("{    \"name\": \"P\",    \"time\": 0,    \"maxTokenId\": 0,    \"arcs\": [      {        \"sourceNode\": 0,        \"targetNode\": 1,        \"type\": 0      }    ],    \"places\": [      {        \"placeId\": 1,        \"name\": \"Place\",        \"maxAge\": 0,        \"isUrgent\": false,        \"numberOfTokens\": 2147483647,        \"tokens\": [          {            \"id\": 0,            \"tokenId\": 0,            \"name\": \"string\",            \"age\": 0          }        ]      }    ],    \"transitions\": [      {        \"transitionId\": 0,        \"name\": \"0\"      }    ]  }", System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/v1/Model", httpContent);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
    }
    
}


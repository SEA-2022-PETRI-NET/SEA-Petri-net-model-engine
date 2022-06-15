using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace PetriNetEngine.Tests.IntegrationTests;

public class ModelPetriNetTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    public ModelPetriNetTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task GET_model()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        var response = await client.GetAsync("api/v1/Model");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task POST_model()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        StringContent httpContent = new StringContent("{    \"name\": \"P\",    \"time\": 0,    \"maxTokenId\": 0,    \"arcs\": [      {        \"sourceNode\": 0,        \"targetNode\": 1,        \"type\": 0      }    ],    \"places\": [      {        \"placeId\": 1,        \"name\": \"Place\",        \"maxAge\": 0,        \"isUrgent\": false,        \"numberOfTokens\": 2147483647,        \"tokens\": [          {            \"id\": 0,            \"tokenId\": 0,            \"name\": \"string\",            \"age\": 0          }        ]      }    ],    \"transitions\": [      {        \"transitionId\": 0,        \"name\": \"0\"      }    ]  }", System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/v1/Model", httpContent);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
    }
    
    [Fact]
    public async Task GET_model_id()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        StringContent httpContent = new StringContent("{    \"name\": \"P\",    \"time\": 0,    \"maxTokenId\": 0,    \"arcs\": [      {        \"sourceNode\": 0,        \"targetNode\": 1,        \"type\": 0      }    ],    \"places\": [      {        \"placeId\": 1,        \"name\": \"Place\",        \"maxAge\": 0,        \"isUrgent\": false,        \"numberOfTokens\": 2147483647,        \"tokens\": [          {            \"id\": 0,            \"tokenId\": 0,            \"name\": \"string\",            \"age\": 0          }        ]      }    ],    \"transitions\": [      {        \"transitionId\": 0,        \"name\": \"0\"      }    ]  }", System.Text.Encoding.UTF8, "application/json");

        var res = await client.PostAsync("api/v1/Model", httpContent);
        var response = await client.GetAsync("api/v1/Model/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var response1 = await client.GetAsync("api/v1/Model/2147483647");
        Assert.Equal(HttpStatusCode.NotFound, response1.StatusCode);
        
    }
    
    [Fact]
    public async Task Post_Validate()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        StringContent httpContent = new StringContent("{    \"name\": \"P\",    \"time\": 0,    \"maxTokenId\": 0,    \"arcs\": [      {        \"sourceNode\": 0,        \"targetNode\": 1,        \"type\": 0      }    ],    \"places\": [      {        \"placeId\": 1,        \"name\": \"Place\",        \"maxAge\": 0,        \"isUrgent\": false,        \"numberOfTokens\": 2147483647,        \"tokens\": [          {            \"id\": 0,            \"tokenId\": 0,            \"name\": \"string\",            \"age\": 0          }        ]      }    ],    \"transitions\": [      {        \"transitionId\": 0,        \"name\": \"0\"      }    ]  }", System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/v1/Model/validate-petri-net", httpContent);
        _testOutputHelper.WriteLine(response.ToString());
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        
    }
    
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using Match = Moq.Match;

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
    public async Task PUTSimulationResponse()
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
    
    [Fact]
    public async Task PUTSimulationFire()
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

        var response = await client.PutAsync("/api/v1/Simulation/" + id.ToString() +"/"+ 0.ToString(), null);
        _testOutputHelper.WriteLine(response.ToString());


        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task PUTSimulationFireValues()
    {
        await using var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();
        
        StringContent httpContent = new StringContent("{    \"name\": \"P\",    \"time\": 0,    \"maxTokenId\": 0,    \"arcs\": [        {            \"sourceNode\": 1,            \"targetNode\": 0,            \"type\": 0        },        {            \"sourceNode\": 0,            \"targetNode\": 2,            \"type\": 0        }    ],    \"places\": [        {            \"placeId\": 1,            \"name\": \"1\",            \"maxAge\": 0,            \"isUrgent\": false,            \"numberOfTokens\": 10,            \"tokens\": [ ]        },        {            \"placeId\": 2,            \"name\": \"2\",            \"maxAge\": 0,            \"isUrgent\": false,            \"numberOfTokens\": 10,            \"tokens\": [ ]        }    ],    \"transitions\": [        {            \"transitionId\": 0,            \"name\": \"0\"        }    ]}", System.Text.Encoding.UTF8, "application/json");

        var res = await client.PostAsync("api/v1/Model", httpContent);

        string[] subs = res.Headers.Location.ToString().Split('/');
        List<string> list = new List<string>(subs);
        string last = list[^1];
        int id = Int32.Parse(last);

        var response = await client.PutAsync("/api/v1/Simulation/" + id.ToString() +"/"+ 0.ToString(), null);
        
        var getResponse = await client.GetAsync("api/v1/Model/" + id.ToString());
        
        
        var stream = getResponse.Content.ReadAsStream();
        StreamReader readStream = new StreamReader (stream, Encoding.UTF8);
        var streamString = readStream.ReadToEnd();
        
        string search = "(?<=numberOfTokens[\\][\"].)\\d+";
        var match = Regex.Matches(streamString, search);


        var expectedNumberOfTokens1 =
            "{\"id\":"+id.ToString() + ",\"name\":\"P\",\"time\":null,\"maxTokenId\":null,\"arcs\":[{\"sourceNode\":0,\"targetNode\":2,\"type\":0},{\"sourceNode\":1,\"targetNode\":0,\"type\":0}],\"places\":[{\"id\":" + id.ToString() + ",\"placeId\":1,\"name\":\"1\",\"maxAge\":0,\"isUrgent\":false,\"numberOfTokens\":9,\"tokens\":[],\"position\":null},{\"id\":" + id.ToString() + ",\"placeId\":2,\"name\":\"2\",\"maxAge\":0,\"isUrgent\":false,\"numberOfTokens\":11,\"tokens\":[],\"position\":null}],\"transitions\":[{\"id\":"+id.ToString() + ",\"transitionId\":0,\"name\":\"0\",\"position\":null}]}";
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("9", match[0].ToString());
        Assert.Equal("11", match[1].ToString());
        
    }
    

}

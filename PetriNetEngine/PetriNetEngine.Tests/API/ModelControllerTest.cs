using System;
using System.Collections.Generic;
using Moq;
using PetriNetEngine.API;
using PetriNetEngine.Application;
using PetriNetEngine.Domain.Services;
using SEA_Models.PetriNet;
using Xunit;
using Xunit.Abstractions;

namespace PetriNetEngine.Tests.API;

public class ModelControllerTest
{
    
    private readonly ITestOutputHelper _testOutputHelper;
    public ModelControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Get_All()
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
    
    [Fact]
    public void Get()
    {
        //Given
        var mock = new Mock<IModelPetriNetService>();
        var mock2 = new Mock<IValidatePetriNetService>();
        ModelController mc = new ModelController(mock.Object, mock2.Object);
        //When
        var res = mc.Get(1);
        //Then
        mock.Verify(x=>x.GetPetriNet(1),Times.Once());
    }
    
    [Fact]
    public void Create()
    {
        //Given
        var mockRepository = new Mock<IPetriNetRepository>();
        var mockModel = new Mock<IModelPetriNetService>();
        var mockValidate = new Mock<IValidatePetriNetService>();
        ModelController mc = new ModelController(mockModel.Object, mockValidate.Object);
        Arc a = new Arc
        {
            SourceNode = 0,
            TargetNode = 1,
            Type = ArcType.Normal
        };
        Transition t = new Transition
        {
            TransitionId = 0,
            Name = "0"
        };
        Place p1 = new Place
        {
            PlaceId = 1,
            Name = "Place",
            maxAge = 0,
            isUrgent = false,
            NumberOfTokens = 10000,
            Tokens = new List<Token>()
            {
                new Token
                {
                    Id = 0,
                    TokenId = 0,
                    Age = 0,
                    Name = "S"
                }
            }
        };


        PetriNet p = new PetriNet
        {
            Places = new List<Place>()
            {
                p1
            },
            Arcs = new List<Arc>()
            {
                a
            },
            Transitions = new List<Transition>()
            {
                t
            },
            Id = 1,
            Name = "hej",
            MaxTokenId = 299999999,
            Time = 10000
        };
        _testOutputHelper.WriteLine(p.ToString());
        _testOutputHelper.WriteLine(p.Arcs.ToString());
        _testOutputHelper.WriteLine(p.Arcs[0].ToString());
        _testOutputHelper.WriteLine(p.Transitions.ToString());
        _testOutputHelper.WriteLine(p.Transitions[0].ToString());
        _testOutputHelper.WriteLine(p.Places.ToString());
        _testOutputHelper.WriteLine(p.Places[0].ToString());
        //When
        var res = mc.Create(p);
        //Then
        mockModel.Verify(x=>x.CreateNetPetriNet(p),Times.Once());
    }



}



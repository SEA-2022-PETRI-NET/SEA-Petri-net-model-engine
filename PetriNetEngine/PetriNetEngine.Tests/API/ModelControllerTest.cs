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
        var mc = new ModelController(mockModel.Object, mockValidate.Object);
        var a = new Arc
        {
            PetriNetId = 1,
            SourceNode = 0,
            TargetNode = 1,
            Type = ArcType.Normal
        };
        var t = new Transition
        {
            PetriNetId = 1,
            TransitionId = 0,
            Name = "0"
        };
        var p1 = new Place
        {
            PetriNetId = 1,
            PlaceId = 1,
            Name = "Place",
            maxAge = 0,
            isUrgent = false,
            NumberOfTokens = 10000,
            Tokens = new List<Token>()
            {
                new Token
                {
                    PetriNetId = 1,
                    Id = 0,
                    TokenId = 0,
                    Age = 0,
                    Name = "S"
                }
            }
        };

        var p = new PetriNet
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
        mockModel.Setup(x => x.CreateNetPetriNet(p)).Returns(p);
        
        //When
        var res = mc.Create(p);
        _testOutputHelper.WriteLine(res.ToString());

        //Then
        mockModel.Verify(x=>x.CreateNetPetriNet(p),Times.Once());
    }
    
    [Fact]
    public void Update()
    {
        //Given
        var mockModel = new Mock<IModelPetriNetService>();
        var mockValidate = new Mock<IValidatePetriNetService>();
        var mc = new ModelController(mockModel.Object, mockValidate.Object);
        var a = new Arc
        {
            PetriNetId = 1,
            SourceNode = 0,
            TargetNode = 1,
            Type = ArcType.Normal
        };
        var t = new Transition
        {
            PetriNetId = 1,
            TransitionId = 0,
            Name = "0"
        };
        var p1 = new Place
        {
            PetriNetId = 1,
            PlaceId = 1,
            Name = "Place",
            maxAge = 0,
            isUrgent = false,
            NumberOfTokens = 10000,
            Tokens = new List<Token>()
            {
                new Token
                {
                    PetriNetId = 1,
                    Id = 0,
                    TokenId = 0,
                    Age = 0,
                    Name = "S"
                }
            }
        };

        var p = new PetriNet
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
        var res = mc.Update(1, p);
        //Then
        mockModel.Verify(x=>x.UpdatePetriNet(1,p),Times.Once());
    }
    
    
    [Fact]
    public void Validate()
    {
        //Given
        var mockModel = new Mock<IModelPetriNetService>();
        var mockValidate = new Mock<IValidatePetriNetService>();
        var mc = new ModelController(mockModel.Object, mockValidate.Object);
        var a = new Arc
        {
            PetriNetId = 1,
            SourceNode = 0,
            TargetNode = 1,
            Type = ArcType.Normal
        };
        var t = new Transition
        {
            PetriNetId = 1,
            TransitionId = 0,
            Name = "0"
        };
        var p1 = new Place
        {
            PetriNetId = 1,
            PlaceId = 1,
            Name = "Place",
            maxAge = 0,
            isUrgent = false,
            NumberOfTokens = 10000,
            Tokens = new List<Token>()
            {
                new Token
                {
                    PetriNetId = 1,
                    Id = 0,
                    TokenId = 0,
                    Age = 0,
                    Name = "S"
                }
            }
        };

        var p = new PetriNet
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
        var res = mc.Validate(p);
        //Then
        mockValidate.Verify(x=>x.Validate(p),Times.Once());
    }
    
    [Fact]
    public void Delete()
    {
        //Given
        var mockModel = new Mock<IModelPetriNetService>();
        var mockValidate = new Mock<IValidatePetriNetService>();
        var mc = new ModelController(mockModel.Object, mockValidate.Object);
        //When
        var res = mc.Delete(1);
        //Then
        mockModel.Verify(x=>x.DeletePetriNet(1),Times.Once());
    }

    
    
    



}



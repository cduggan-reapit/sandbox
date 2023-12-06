using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Queries.GetAllAddresses;
using Sandbox.Api.Web.Controllers.Addresses.V1;
using Sandbox.Api.Web.Controllers.Addresses.V1.Models;
using Sandbox.Api.Web.Errors;

namespace Sandbox.Api.Tests.Web.Controllers.Addresses.V1;

public class AddressesControllerTests
{
    private readonly Mock<IMediator> _mediator;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<AddressesController>> _logger;

    public AddressesControllerTests()
    {
        _mediator = new Mock<IMediator>();
        _mapper = TestHelpers.AutomapperHelper.GetMapper();
        _logger = new Mock<ILogger<AddressesController>>();
    }

    // GET: /addresses
    
    [Fact]
    public async Task GetAllAddresses_ShouldReturnAllAddresses_WhenMediatrRequestHandledSuccessfully()
    {
        var addresses = new ReadAddressDto[]
        {
            new() { Id = Guid.NewGuid(), Created = DateTimeOffset.UnixEpoch, Modified = DateTimeOffset.Now },
            new() { Id = Guid.NewGuid(), Created = DateTimeOffset.UnixEpoch, Modified = DateTimeOffset.Now }
        };
        
        _mediator.Setup(m => m.Send(It.IsAny<GetAllAddressesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(addresses)
            .Verifiable();

        var sut = CreateSut();

        var response = await sut.GetAllAddresses();
        
        var result = response as OkObjectResult;
        result.Should().NotBeNull();

        var content = result!.Value as IEnumerable<ReadAddressDto>;
        content.Should().BeEquivalentTo(addresses);
        
        _mediator.Verify(m => m.Send(It.IsAny<GetAllAddressesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task GetAllAddresses_ShouldReturnErrorModel_WhenExceptionThrown()
    {
        var exception = new FormatException("Test exception");
        var expectedModel = exception.GetGenericErrorModel();
            
        _mediator.Setup(m => m.Send(It.IsAny<GetAllAddressesQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);

        var sut = CreateSut();

        var response = await sut.GetAllAddresses();
        
        var result = response as ObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(500);
        
        var actualModel = result.Value as ErrorModel;
        actualModel.Should().BeEquivalentTo(expectedModel);
    }
    
    // POST: /addresses
    // Create Address should return NoContent
    // Create Address should return BadRequest
    // Create Address should return InternalServerError

    [Fact] public async Task GetAllAddresses_ShouldReturnAllAddresses_WhenValidModelProvided()
    {
        var model = new CreateAddressRequestModel(
            AddressType: "commercial",
            Number: "Oficina Principal Dirección",
            Street: "Av. Comandante Espinar 331",
            City: "Lima",
            County: "Miraflores",
            State: null,
            Country: "Peru",
            PostCode: "15046"
        );

        var expectedDto = new ReadAddressDto
        {
            AddressType = model.AddressType,
            Number = model.Number,
            City = model.City,
            Country = model.Country,
            County = model.County,
            PostCode = model.PostCode,
            Id = Guid.NewGuid(),
            Created = DateTimeOffset.UnixEpoch,
            Modified = DateTimeOffset.Now,
            EntityTag = "TestEntityTag"
        };
        
        _mediator.Setup(m => m.Send(It.IsAny<CreateAddressCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto)
            .Verifiable();

        var sut = CreateSut();

        var response = await sut.CreateAddress(model);
        
        var result = response as CreatedAtActionResult;
        result.Should().NotBeNull();

        var resultModel = result!.Value as ReadAddressDto;
        resultModel.Should().BeEquivalentTo(expectedDto);
        
        _mediator.Verify(m => m.Send(It.IsAny<CreateAddressCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    private AddressesController CreateSut() => new AddressesController(_mediator.Object, _mapper, _logger.Object);
}
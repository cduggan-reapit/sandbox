using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Queries.GetAllAddresses;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Web.Controllers.Addresses.V1;
using Sandbox.Api.Web.Controllers.Addresses.V1.Models;
using FluentValidation.Results;
using Sandbox.Api.Web.Errors.Application;
using Sandbox.Api.Web.Errors.Validation;
using ValidationException = FluentValidation.ValidationException;

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

        var expected = _mapper.Map<IEnumerable<ReadAddressResponseModel>>(addresses);
        
        _mediator.Setup(m => m.Send(It.IsAny<GetAllAddressesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(addresses)
            .Verifiable();

        var sut = CreateSut();

        var response = await sut.GetAllAddresses();
        
        var result = response as OkObjectResult;
        result.Should().NotBeNull();

        var content = result!.Value as IEnumerable<ReadAddressResponseModel>;
        content.Should().BeEquivalentTo(expected);
        
        _mediator.Verify(m => m.Send(It.IsAny<GetAllAddressesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task GetAllAddresses_ShouldReturnErrorModel_WhenExceptionThrown()
    {
        var exception = new FormatException("Test exception");
        var expectedModel = exception.GetExceptionErrorModel();
            
        _mediator.Setup(m => m.Send(It.IsAny<GetAllAddressesQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);

        var sut = CreateSut();

        var response = await sut.GetAllAddresses();
        
        var result = response as ObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(500);
        
        var actualModel = result.Value as ApplicationErrorModel;
        actualModel.Should().BeEquivalentTo(expectedModel);
    }
    
    // POST: /addresses

    [Fact] 
    public async Task CreateAddress_ShouldReturnAddress_WhenValidModelProvided()
    {
        var model = new WriteAddressRequestModel(
            AddressType: "commercial",
            Number: "Oficina Principal Dirección",
            Street: "Av. Comandante Espinar 331",
            City: "Lima",
            County: "Miraflores",
            State: null,
            Country: "Peru",
            PostCode: "15046"
        );

        var dto = new ReadAddressDto
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
        
        var expected = _mapper.Map<ReadAddressResponseModel>(dto);
        
        _mediator.Setup(m => m.Send(It.IsAny<CreateAddressCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto)
            .Verifiable();

        var sut = CreateSut();
        
        var response = await sut.CreateAddress(model);
        
        var result = response as CreatedAtActionResult;
        result.Should().NotBeNull();

        var resultModel = result!.Value as ReadAddressResponseModel;
        resultModel.Should().BeEquivalentTo(expected);
        
        _mediator.Verify(m => m.Send(It.IsAny<CreateAddressCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task CreateAddress_ShouldReturnNotFound_WhenAddressNotFound()
    {
        var id = Guid.NewGuid();
        var exception = new NotFoundException(nameof(Address), id);
        var expectedResponse = exception.GetNotFoundErrorModel();
        
        var model = new WriteAddressRequestModel(
            AddressType: "commercial",
            Number: "Oficina Principal Dirección",
            Street: "Av. Comandante Espinar 331",
            City: "Lima",
            County: "Miraflores",
            State: null,
            Country: "Peru",
            PostCode: "15046"
        );
        
        _mediator.Setup(m => m.Send(It.IsAny<CreateAddressCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);

        var sut = CreateSut();
        var response = await sut.CreateAddress(model);

        var result = response as ObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(404);

        var content = result.Value as ApplicationErrorModel;
        content.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateAddress_ShouldReturnBadRequest_WhenValidationFails()
    {
        var exception = new ValidationException(new List<ValidationFailure>
        {
            new (){ PropertyName = "Property", ErrorMessage = "PropertyError"}
        });
        
        var expectedResponse = exception.Errors.GetValidationErrorModel();
        
        var model = new WriteAddressRequestModel(
            AddressType: "AddressType",
            Number: "Number",
            Street: "Street",
            City: "City",
            County: "County",
            State: "State",
            Country: "Country",
            PostCode: "PostCode"
        );
        
        _mediator.Setup(m => m.Send(It.IsAny<CreateAddressCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);

        var sut = CreateSut();
        var response = await sut.CreateAddress(model);

        var result = response as ObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(400);

        var content = result.Value as ValidationErrorModel;
        content.Should().BeEquivalentTo(expectedResponse);
    }
    
    [Fact]
    public async Task CreateAddress_ShouldReturnInternalServerErrorResult_WhenExceptionCaught()
    {
        var exception = new ApplicationException("Test Exception");
        var expectedResponse = exception.GetExceptionErrorModel();
        
        var model = new WriteAddressRequestModel(
            AddressType: "AddressType",
            Number: "Number",
            Street: "Street",
            City: "City",
            County: "County",
            State: "State",
            Country: "Country",
            PostCode: "PostCode"
        );
        
        _mediator.Setup(m => m.Send(It.IsAny<CreateAddressCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);

        var sut = CreateSut();
        var response = await sut.CreateAddress(model);

        var result = response as ObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(500);

        var content = result.Value as ApplicationErrorModel;
        content.Should().BeEquivalentTo(expectedResponse);
    }
    
    private AddressesController CreateSut() 
        => new (_mediator.Object, _mapper, _logger.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            }
        };
}
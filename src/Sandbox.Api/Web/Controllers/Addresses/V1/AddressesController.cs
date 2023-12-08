using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using Sandbox.Api.Core.Addresses.Commands.DeleteAddressById;
using Sandbox.Api.Core.Addresses.Commands.UpdateAddressById;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Queries.GetAddressById;
using Sandbox.Api.Core.Addresses.Queries.GetAllAddresses;
using Sandbox.Api.Web.Controllers.Addresses.V1.Models;
using Sandbox.Api.Web.Errors.Application;
using Sandbox.Api.Web.Errors.Validation;

namespace Sandbox.Api.Web.Controllers.Addresses.V1;

/// <summary>
/// Controller exposing information and management features for addresses
/// </summary>
[ApiVersion(1)]
[ApiVersion(2)]
public class AddressesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<AddressesController> _logger;

    /// <summary>
    /// Initialize a new <see cref="AddressesController"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    public AddressesController(IMediator mediator, IMapper mapper, ILogger<AddressesController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Retrieve a list of all addresses
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReadAddressResponseModel>), 200)]
    [ProducesErrorResponseType(typeof(ApplicationErrorModel))]
    public async Task<IActionResult> GetAllAddresses()
    {
        _logger.LogInformation("Attempting to fetch all addresses");
        try
        {
            var dtoCollection = await _mediator.Send(new GetAllAddressesQuery());
            var responseModels = _mapper.Map<IEnumerable<ReadAddressResponseModel>>(dtoCollection);
            return Ok(responseModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetExceptionErrorModel());
        }
    }
    
    /// <summary>
    /// Retrieve an address by it's identifier
    /// </summary>
    /// <param name="id">The unique identifier of the address to retrieve</param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReadAddressResponseModel), 200)]
    [ProducesResponseType(typeof(ApplicationErrorModel), 404)]
    [ProducesErrorResponseType(typeof(ApplicationErrorModel))]
    public async Task<IActionResult> GetAddressById(Guid id)
    {
        _logger.LogInformation("Attempting to fetch address with Id: '{id}'", id);
        try
        {
            var dto = await _mediator.Send(new GetAddressByIdQuery(id));

            SetResponseHeaderETag(dto.EntityTag);

            var model = _mapper.Map<ReadAddressResponseModel>(dto);
            return Ok(model);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.GetNotFoundErrorModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetExceptionErrorModel());
        }
    }
    
    /// <summary>
    /// Retrieve the headers which would be returned when getting an address by it's identifier
    /// </summary>
    /// <param name="id">The unique identifier of the address for which to retrieve headers</param>
    /// <returns></returns>
    [HttpHead("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ApplicationErrorModel), 404)]
    [ProducesErrorResponseType(typeof(ApplicationErrorModel))]
    public async Task<IActionResult> GetAddressByIdHeaders(Guid id)
    {
        _logger.LogInformation("Attempting to fetch headers for address with Id: '{id}'", id);
        try
        {
            var dto = await _mediator.Send(new GetAddressByIdQuery(id));
            
            SetResponseHeaderETag(dto.EntityTag);

            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.GetNotFoundErrorModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetExceptionErrorModel());
        }
    }
    
    /// <summary>
    /// Create a new address
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ReadAddressDto), 200)]
    [ProducesResponseType(typeof(ValidationErrorModel), 400)]
    [ProducesErrorResponseType(typeof(ApplicationErrorModel))]
    public async Task<IActionResult> CreateAddress([FromBody] WriteAddressRequestModel model)
    {
        _logger.LogInformation("Attempting to create address");
        try
        {
            var command = _mapper.Map<CreateAddressCommand>(model);
            var dto = await _mediator.Send(command);

            SetResponseHeaderETag(dto.EntityTag);

            var responseModel = _mapper.Map<ReadAddressResponseModel>(dto);

            return CreatedAtAction(nameof(GetAddressById), new { id = responseModel.Id }, responseModel);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors.GetValidationErrorModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetExceptionErrorModel());
        }
    }
    
    /// <summary>
    /// Update all properties of the address matching the given identifier
    /// </summary>
    /// <param name="id">The unique identifier of the address to update</param>
    /// <param name="model">The property values to be applied to the address</param>
    /// <param name="etag">Identifier for a specific version of an address</param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ValidationErrorModel), 400)]
    [ProducesResponseType(typeof(ApplicationErrorModel), 404)]
    [ProducesResponseType(typeof(ApplicationErrorModel), 409)]
    [ProducesErrorResponseType(typeof(ApplicationErrorModel))]
    public async Task<IActionResult> UpdateAddressById(Guid id, 
        [FromBody] WriteAddressRequestModel model, 
        [FromHeader(Name = "If-Match")] string? etag = null)
    {
        _logger.LogInformation("Attempting to update address with Id: '{id}'", id);
        try
        {
            var command = new UpdateAddressByIdCommand(Id: id,
                ETag: etag ?? string.Empty,
                AddressType: model.AddressType,
                Number: model.Number,
                Street: model.Street,
                City: model.City,
                County: model.County,
                State: model.State,
                Country: model.Country,
                PostCode: model.PostCode
            );

            var addressDto = await _mediator.Send(command);
            
            SetResponseHeaderETag(addressDto.EntityTag);

            return NoContent();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors.GetValidationErrorModel());
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.GetNotFoundErrorModel());
        }
        catch (EntityConflictException ex)
        {
            return Conflict(ex.GetConflictErrorModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetExceptionErrorModel());
        }
    }
    
    /// <summary>
    /// Delete the address matching the given identifier
    /// </summary>
    /// <param name="id">The unique identifier of the address to delete</param>
    /// <param name="etag">Identifier for a specific version of an address</param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ApplicationErrorModel), 404)]
    [ProducesResponseType(typeof(ApplicationErrorModel), 409)]
    [ProducesErrorResponseType(typeof(ApplicationErrorModel))]
    public async Task<IActionResult> DeleteAddressById(Guid id, [FromHeader(Name = "If-Match")] string? etag = null)
    {
        _logger.LogInformation("Attempting to delete address with Id: '{id}'", id);
        try
        {
            await _mediator.Send(new DeleteAddressByIdCommand(id, etag));
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.GetNotFoundErrorModel());
        }
        catch (EntityConflictException ex)
        {
            return Conflict(ex.GetConflictErrorModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetExceptionErrorModel());
        }
    }
}
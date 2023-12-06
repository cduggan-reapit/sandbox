using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using Sandbox.Api.Core.Addresses.Commands.DeleteAddressById;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Queries.GetAddressById;
using Sandbox.Api.Core.Addresses.Queries.GetAllAddresses;
using Sandbox.Api.Web.Controllers.Addresses.V1.Models;
using Sandbox.Api.Web.Errors;

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
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReadAddressResponseModel>), 200)]
    [ProducesErrorResponseType(typeof(ErrorModel))]
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
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetGenericErrorModel());
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReadAddressResponseModel), 200)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(ErrorModel))]
    public async Task<IActionResult> GetAddressById(Guid id)
    {
        _logger.LogInformation("Attempting to fetch address with Id: '{id}'", id);
        try
        {
            var dto = await _mediator.Send(new GetAddressByIdQuery(id));
            
            if(dto == null)
                return NotFound();

            SetResponseHeaderETag(dto.EntityTag);
            
            var model = _mapper.Map<ReadAddressResponseModel>(dto);
            return Ok(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetGenericErrorModel());
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpHead("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(ErrorModel))]
    public async Task<IActionResult> GetAddressByIdHeaders(Guid id)
    {
        _logger.LogInformation("Attempting to fetch headers for address with Id: '{id}'", id);
        try
        {
            var dto = await _mediator.Send(new GetAddressByIdQuery(id));
            
            if(dto == null)
                return NotFound();
            
            SetResponseHeaderETag(dto.EntityTag);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetGenericErrorModel());
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ReadAddressDto), 200)]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesErrorResponseType(typeof(ErrorModel))]
    public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequestModel model)
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
            return BadRequest(ex.Errors.GetErrorModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetGenericErrorModel());
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="etag"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [ProducesErrorResponseType(typeof(ErrorModel))]
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
            return NotFound(ex.GetErrorModel());
        }
        catch (EntityConflictException ex)
        {
            return Conflict(ex.GetErrorModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetGenericErrorModel());
        }
    }
}
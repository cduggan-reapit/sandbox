using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Queries.GetAllAddresses;
using Sandbox.Api.Web.Controllers.Addresses.V1.Models;
using Sandbox.Api.Web.Errors;

namespace Sandbox.Api.Web.Controllers.Addresses.V1;

/// <summary>
/// Controller exposing information and management features for addresses
/// </summary>
[ApiVersion(1)]
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
    [ProducesResponseType(typeof(IEnumerable<ReadAddressDto>), 200)]
    [ProducesErrorResponseType(typeof(ErrorModel))]
    public async Task<IActionResult> GetAllAddresses()
    {
        _logger.LogInformation("Attempting to fetch all addresses");
        try
        {
            var result = await _mediator.Send(new GetAllAddressesQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetErrorModel());
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
            var result = await _mediator.Send(command);

            // TODO: Add Etag to header & map to ResponseModel
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors.GetErrorModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.GetErrorModel());
        }
    }
}
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Queries.GetAllAddresses;
using Sandbox.Api.Web.Errors;

namespace Sandbox.Api.Web.Controllers.Addresses.V1._0;

/// <summary>
/// Controller exposing information and management features for addresses
/// </summary>
[ApiVersion(1)]
public class AddressesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<AddressesController> _logger;

    /// <summary>
    /// Initialize a new <see cref="AddressesController"/>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public AddressesController(IMediator mediator, ILogger<AddressesController> logger)
    {
        _mediator = mediator;
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
}
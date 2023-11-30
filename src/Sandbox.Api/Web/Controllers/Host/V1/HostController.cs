using System.Reflection;
using Asp.Versioning;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Api.Core.Queries.GetHostVersion;
using Sandbox.Api.Web.Errors;
using Swashbuckle.AspNetCore.Filters;

namespace Sandbox.Api.Web.Controllers.Host.V1;

[ApiVersion(1.0)]
public class HostController : BaseController
{
    private readonly ILogger<HostController> _logger;
    private readonly IMediator _mediator;

    public HostController(ILogger<HostController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get the version number of the Sandbox Api
    /// </summary>
    /// <returns>Version number in the format major.minor.build.revision</returns>
    [HttpGet("version")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetHostVersion()
    {
        _logger.LogInformation("Attempting to retrieve Host version");
        try
        {
            return Ok(await _mediator.Send(new GetHostVersionQuery(false)));
        }
        catch (ValidationException ex)
        {
            _logger.LogInformation("Validation failed when retrieving Host version");
            return BadRequest(ErrorModelFactory.GetErrorModel(ex.Errors));
        }
        catch (Exception ex)
        {
            _logger.LogError("A system error occurred when retrieving Host version: {message} ({type})", ex.Message, ex.GetType().Name);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Get the version number of the Sandbox Api
    /// </summary>
    /// <returns>Version number in the format major.minor.build.revision</returns>
    [HttpGet("api-version")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetApiVersion(ApiVersion version)
        => Ok(version.ToString());
}
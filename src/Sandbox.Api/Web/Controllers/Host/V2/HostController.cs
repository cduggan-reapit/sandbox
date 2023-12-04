﻿using Asp.Versioning;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.Api.Core.Hosts.Queries.GetHostVersion;
using Sandbox.Api.Web.Errors;

namespace Sandbox.Api.Web.Controllers.Host.V2;

/// <summary>
/// Controller exposing information about the application and API versions
/// </summary>
[ApiVersion(2.0)]
public class HostController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<HostController> _logger;

    /// <summary>
    /// Initialize a new instance of <see cref="HostController"/>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public HostController(ILogger<HostController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    /// <summary>
    /// Get the version number of the Sandbox Api
    /// </summary>
    /// <returns>Version number in the format major.minor.build.revision</returns>
    [HttpGet("app-version")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
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
            return BadRequest(ex.Errors.GetErrorModel());
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
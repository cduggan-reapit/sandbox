using System.Reflection;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Sandbox.Api.Web.Controllers.Host.V1;

[ApiVersion(1.0)]
public class HostController : BaseController
{
    /// <summary>
    /// Get the version number of the Sandbox Api
    /// </summary>
    /// <returns>Version number in the format major.minor.build.revision</returns>
    [HttpGet("version")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetHostVersion()
        => Ok(Assembly.GetExecutingAssembly().GetName().Version?.ToString());

    /// <summary>
    /// Get the version number of the Sandbox Api
    /// </summary>
    /// <returns>Version number in the format major.minor.build.revision</returns>
    [HttpGet("api-version")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetApiVersion(ApiVersion version)
        => Ok(version.ToString());
}
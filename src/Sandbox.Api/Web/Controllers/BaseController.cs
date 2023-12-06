using Microsoft.AspNetCore.Mvc;

namespace Sandbox.Api.Web.Controllers;

/// <summary>
/// Base class for Sandbox API controllers 
/// </summary>
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Adds the provided ETag to the response headers
    /// </summary>
    /// <param name="etag"></param>
    protected void SetResponseHeaderETag(string etag)
    {
        Response.Headers.ETag = etag;
    }
}
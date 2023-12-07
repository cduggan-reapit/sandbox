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
        // Wrap ETag in quotes per RFC: https://www.rfc-editor.org/rfc/rfc7232
        Response.Headers.ETag = $"\"etag\"";
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Sandbox.Api.Web.Controllers;

/// <summary>
/// Base class for Sandbox API controllers 
/// </summary>
[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
}
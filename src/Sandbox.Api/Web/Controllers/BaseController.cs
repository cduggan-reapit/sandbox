using Microsoft.AspNetCore.Mvc;

namespace Sandbox.Api.Web.Controllers;

[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    
}
using Microsoft.AspNetCore.Mvc;

namespace Sandbox.Api.Web.Controllers;

[ApiController]
[Route("/api/{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    
}
using Incoding.CQRS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PogaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected IDispatcher Dispatcher => (IDispatcher)HttpContext.RequestServices.GetService(typeof(IDispatcher));
    }
}

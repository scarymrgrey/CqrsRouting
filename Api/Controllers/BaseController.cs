using Incoding.CQRS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace PogaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected IDispatcher Dispatcher => (IDispatcher)HttpContext.RequestServices.GetService(typeof(IDispatcher));
        protected ILogger Logger => Log.Logger;
        protected int CurrentUserId => int.Parse(HttpContext.User.Identity.Name);
    }
}

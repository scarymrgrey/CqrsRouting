using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operations.Commands;
using Operations.Queries;

namespace PogaWebApi.Controllers
{
    public class AccountController : BaseController
    { 
        [HttpPost("signin")]
        [AllowAnonymous]
        public IActionResult SignIn([FromBody] SignInQuery query)
        {
            return Ok(Dispatcher.Query(query));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(Dispatcher.Query(new GetTestSpQuery()));
        }

        [AllowAnonymous]
        [HttpPost("simpleroute")]
        public object Test2([FromBody] TestQuery query)
        {
            return Dispatcher.Query(query);
        }

        [AllowAnonymous]
        [HttpPost("simpleroute2")]
        public object Test3([FromBody] TestQuery query)
        {
            return 3;
        }
    }
    
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operations.Queries;
using Operations.Queries.Account;

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
    }
}

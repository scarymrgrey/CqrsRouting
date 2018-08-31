using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operations.Queries.Client;
using Incoding.CQRS;
using Operations.Commands.Client;

namespace PogaWebApi.Controllers
{
    public class ClientController : BaseController
    {
        /// <summary>
        /// Client registration step1
        /// </summary>
        /// <param name="command"></param>
        /// <returns>ClientId</returns>
        [HttpPost("step1")]
        [AllowAnonymous]
        public int CreateUser([FromBody] RegisterClientStep1Command command)
        {
            command.UserModifyId = CurrentUserId;
            Dispatcher.Push(command);
            return (int)command.Result;
        }

        /// <summary>
        /// Client registration step2
        /// </summary>
        /// <param name="command"></param>
        /// <returns>ClientId</returns>
        [HttpPost("step2")]
        [AllowAnonymous]
        public int CreateUserSt2([FromBody] RegisterClientStep2Command command)
        {
            command.UserModifyId = CurrentUserId;
            Dispatcher.Push(command);
            return (int)command.Result;
        }
    }
}

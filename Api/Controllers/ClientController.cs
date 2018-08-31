using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operations.Queries.Client;
using Incoding.CQRS;
namespace PogaWebApi.Controllers
{
    public class ClientController : BaseController
    {
        /// <summary>
        /// Client registration
        /// </summary>
        /// <param name="command"></param>
        /// <returns>ClientId</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateClientCommand), 200)]
        public int CreateUser([FromBody] CreateClientCommand command)
        {
            command.UserModifyId = CurrentUserId;
            Dispatcher.Push(command);
            return (int)command.Result;
        }
    }
}

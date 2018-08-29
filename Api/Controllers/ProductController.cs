using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operations.Queries.Product;

namespace PogaWebApi.Controllers
{
    public class ProductController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public object ProductList()
        {
            return Dispatcher.Query(new GetProductListQuery());
        }
    }
}

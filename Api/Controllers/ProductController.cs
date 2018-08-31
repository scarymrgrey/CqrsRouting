using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operations.Queries.Product;

namespace PogaWebApi.Controllers
{
    public class ProductController : BaseController
    {
        [HttpGet]
        public object ProductList()
        {
            return Dispatcher.Query(new GetProductListQuery());
        }

        /// <summary>
        /// Method returns Extention Costs
        /// </summary>
        /// <returns></returns>
        [HttpGet("ext")]
        public object ExtentionCostTable()
        {
            return Dispatcher.Query(new GetExtentionCostTableQuery());
        }
    }
}

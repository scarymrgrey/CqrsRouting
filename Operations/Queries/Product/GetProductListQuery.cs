using System.Collections.Generic;
using System.Linq;
using Incoding.CQRS;
using Operations.Entities;

namespace Operations.Queries.Product
{
   
    public class GetProductListQuery : QueryBase<List<RefDisbursmentValues>>
    {
        protected override List<RefDisbursmentValues> ExecuteResult()
        {
            return Repository.Query<RefDisbursmentValues>().ToList();
        }
    }
}
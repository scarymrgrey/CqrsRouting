using System.Collections.Generic;
using System.Linq;
using Incoding.CQRS;
using Operations.Entities;

namespace Operations.Queries.Product
{
   
    public class GetExtentionCostTableQuery : QueryBase<List<RefExtentionValue>>
    {
        protected override List<RefExtentionValue> ExecuteResult()
        {
            return Repository.Query<RefExtentionValue>().ToList();
        }
    }
}
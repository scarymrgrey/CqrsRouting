using System.Linq;
using Incoding.CQRS;
using Operations.Entities;

namespace Operations.Queries.Client
{
    public class IsClientByPhoneExistQuery : QueryBase<bool>
    {
        public string Phone { get; set; }
        protected override bool ExecuteResult() => Repository.Query<Entities.Client>().Any(r => r.Phone.PHone == Phone);
    }
}
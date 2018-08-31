using System.Linq;
using Incoding.CQRS;
using Operations.Entities;

namespace Operations.Queries.Client
{
    public class IsClientByEmailExistQuery : QueryBase<bool>
    {
        public string Email { get; set; }
        protected override bool ExecuteResult() => Repository.Query<Entities.Client>().Any(r => r.Email.EMail == Email);
    }
}
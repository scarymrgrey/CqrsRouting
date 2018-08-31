using Incoding.CQRS;
using Operations.Entities;

namespace Operations.Queries
{
    public class GetUserByIdQuery : QueryBase<User>
    {
        public int UserId { get; set; }
        protected override User ExecuteResult()
        {
            return Repository.GetById<User>(UserId);
        }
    }
}
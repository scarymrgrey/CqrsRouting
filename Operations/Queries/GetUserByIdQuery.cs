using Incoding.CQRS;
using Operations.Entities;

namespace Operations.Queries
{
    public class GetUserByIdQuery : QueryBase<User>
    {
        protected override User ExecuteResult()
        {
            throw new System.NotImplementedException();
        }

        public int Id { get; set; }
    }
}
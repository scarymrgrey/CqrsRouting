using CQRS.Block.Http;
using Incoding.CQRS;

namespace Operations.Commands
{
    [Post("/api/test/post")]
    public class TestQuery : QueryBase<int>
    {
        public int Value { get; set; }

        protected override int ExecuteResult()
        {
            return Value;
        }
    }
}
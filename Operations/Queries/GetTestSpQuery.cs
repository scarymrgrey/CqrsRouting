using CQRS.Block.Http;
using Incoding.CQRS;

namespace Operations.Queries
{
    /// <summary>
    /// Просто демонстрация того как вызвать SP c параметрами и вернуть результат
    /// </summary>
    [Get("/api/test")]
    public class GetTestSpQuery : QueryBase<object[]>
    {
        protected override object[] ExecuteResult()
        {
            return Repository.ExecuteSp("SimpleTest",5);
        }
    }
}
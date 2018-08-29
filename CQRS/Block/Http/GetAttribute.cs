using System;

namespace CQRS.Block.Http
{
    public class GetAttribute : HttpVerbAttribute
    {
        public GetAttribute(string url) : base(url)
        {
        }
    }
}
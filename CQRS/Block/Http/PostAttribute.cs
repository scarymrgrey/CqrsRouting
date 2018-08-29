using System;

namespace CQRS.Block.Http
{
    public class PostAttribute : HttpVerbAttribute
    {
        public PostAttribute(string url) : base(url)
        {
        }
    }
}
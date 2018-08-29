using System;

namespace CQRS.Block.Http
{
    public class HttpVerbAttribute : Attribute
    {
        public HttpVerbAttribute(string url)
        {
            Url = url;
        }
        public string Url { get; set; }
    }
}
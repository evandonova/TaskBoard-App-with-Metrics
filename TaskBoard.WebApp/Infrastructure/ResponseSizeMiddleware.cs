using System.IO;
using System.Threading.Tasks;
using Prometheus;
using Microsoft.AspNetCore.Http;

namespace TaskBoard.WebApp.Infrastructure
{
    public class ResponseSizeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Histogram histogram;

        public ResponseSizeMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.histogram = Metrics.CreateHistogram("response_size", "Response size in bytes");
        }

        public async Task Invoke(HttpContext context)
        {
            using (var buffer = new MemoryStream())
            {
                var request = context.Request;
                var response = context.Response;

                var bodyStream = response.Body;
                response.Body = buffer;

                await next(context);
                this.histogram
                    .Observe(response.ContentLength ?? buffer.Length);

                buffer.Position = 0;
                await buffer.CopyToAsync(bodyStream);
            }
        }
    }
}

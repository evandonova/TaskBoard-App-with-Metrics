using System.IO;
using System.Threading.Tasks;
using Prometheus;
using Microsoft.AspNetCore.Http;

namespace TaskBoard.WebApp.Infrastructure
{
    public class ResponsesSizeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Histogram histogram;

        public ResponsesSizeMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.histogram = Metrics.CreateHistogram("responses_size", 
                "Responses size in bytes.",
                new HistogramConfiguration
                {
                    // We divide measurements in 10 buckets of 1000 each
                    Buckets = Histogram
                        .LinearBuckets(start: 4000, width: 1000, count: 10)
                });
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

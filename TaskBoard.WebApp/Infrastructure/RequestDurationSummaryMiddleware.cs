using System.Diagnostics;
using System.Threading.Tasks;
using Prometheus;
using Microsoft.AspNetCore.Http;

namespace TaskBoard.WebApp.Infrastructure
{
    public class RequestDurationSummaryMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Summary summary;

        public RequestDurationSummaryMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.summary = Metrics.CreateSummary("request_duration", "Request duration in milliseconds");
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await next(context);

            stopwatch.Stop();
            summary.Observe(stopwatch.Elapsed.TotalSeconds);
        }
    }
}

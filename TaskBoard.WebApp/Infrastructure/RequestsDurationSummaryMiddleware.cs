using System.Diagnostics;
using System.Threading.Tasks;
using Prometheus;
using Microsoft.AspNetCore.Http;

namespace TaskBoard.WebApp.Infrastructure
{
    public class RequestsDurationSummaryMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Summary summary;

        public RequestsDurationSummaryMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.summary = Metrics.CreateSummary("requests_duration", 
                "Requests duration in milliseconds.");
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await next(context);

            stopwatch.Stop();
            this.summary.Observe(stopwatch.Elapsed.TotalSeconds);
        }
    }
}

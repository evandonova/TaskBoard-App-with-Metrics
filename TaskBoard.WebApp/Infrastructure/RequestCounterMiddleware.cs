using System.Threading.Tasks;
using Prometheus;
using Microsoft.AspNetCore.Http;

namespace TaskBoard.WebApp.Infrastructure
{
    public class RequestCounterMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Counter counter;

        public RequestCounterMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.counter = Metrics.CreateCounter("http_requests_count", "Requests count.", 
                    new CounterConfiguration
                    {
                        LabelNames = new[] { "method", "endpoint" }
                    });
        }

        public async Task Invoke(HttpContext context)
        {
            this.counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
            await next(context);
        }
    }
}

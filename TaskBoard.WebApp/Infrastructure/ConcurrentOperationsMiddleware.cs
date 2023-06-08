using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prometheus;

namespace TaskBoard.WebApp.Infrastructure
{
    public class ConcurrentOperationsMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Gauge concurrentOperations;

        public ConcurrentOperationsMiddleware(RequestDelegate next)
        {
            this.next = next;
            concurrentOperations = Metrics.CreateGauge("ConcurrentOperations", "Number of concurrent operations");
        }

        public async Task Invoke(HttpContext context)
        {
            concurrentOperations.Inc(); // Increment the concurrent operations counter

            try
            {
                await next(context);
            }
            finally
            {
                concurrentOperations.Dec(); // Decrement the concurrent operations counter
            }
        }
    }
}

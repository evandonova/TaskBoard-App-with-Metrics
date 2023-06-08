using Microsoft.AspNetCore.Builder;

namespace TaskBoard.WebApp.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequestCounter(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCounterMiddleware>();
        }

        public static IApplicationBuilder UseRequestDurationSummary(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestDurationSummaryMiddleware>();
        }

        public static IApplicationBuilder UseResponseSizeHistogram(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseSizeMiddleware>();
        }

        public static IApplicationBuilder UseConcurrentOperationsGauge(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConcurrentOperationsMiddleware>();
        }
    }
}

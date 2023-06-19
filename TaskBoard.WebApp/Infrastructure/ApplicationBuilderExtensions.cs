using Microsoft.AspNetCore.Builder;

namespace TaskBoard.WebApp.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequestsCounter(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestsCounterMiddleware>();
        }

        public static IApplicationBuilder UseRequestsDurationSummary(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestsDurationSummaryMiddleware>();
        }

        public static IApplicationBuilder UseResponsesSizeHistogram(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponsesSizeMiddleware>();
        }

        public static IApplicationBuilder UseTasksInBoardsGauge(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TasksInBoardsGaugeMiddleware>();
        }
    }
}


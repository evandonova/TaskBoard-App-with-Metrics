using System.Linq;
using TaskBoard.Data;
using Prometheus;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Task = System.Threading.Tasks.Task;

namespace TaskBoard.WebApp.Infrastructure
{
    public class TasksInBoardsGaugeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Gauge tasksGauge;

        public TasksInBoardsGaugeMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.tasksGauge = Metrics
                 .CreateGauge("tasks_in_boards_gauge", 
                 "Number of \"Open\" / \"In Progress\" / \"Done\" tasks.",
                    new GaugeConfiguration
                    {
                        LabelNames = new[] { "task_board_name" }
                    });
        }

        public async Task Invoke(HttpContext context, 
            ApplicationDbContext dbContext)
        {
            UpdateGaugeMetric(dbContext);
            await next(context);
        }

        private void UpdateGaugeMetric(ApplicationDbContext dbContext)
        {
            var boards = dbContext.Boards.Include(b => b.Tasks);

            foreach (var board in boards)
            {
                var tasksCount = board.Tasks.Count();
                this.tasksGauge.WithLabels(board.Name).Set(tasksCount);
            }
        }
    }
}

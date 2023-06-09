using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Prometheus;

namespace TaskBoard.WebApp.Infrastructure
{
    public class LoggedInUsersGaugeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Gauge loggedInUsersGauge;
        private readonly HashSet<string> loggedInUsers = new HashSet<string>();

        public LoggedInUsersGaugeMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.loggedInUsersGauge = Metrics
                 .CreateGauge("logged_in_users_gauge", "Number of currently logged-in users.");
        }

        public async Task Invoke(HttpContext context)
        {
            string userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // If user logs out, remove them from collection
            if (context.Request.Path.Value == "/Identity/Account/Logout")
            {
                this.loggedInUsers.Remove(userId);
            }
            else if(userId != null)
            {
                this.loggedInUsers.Add(userId);
            }

            this.loggedInUsersGauge.Set(this.loggedInUsers.Count);

            await next(context);
        }
    }
}

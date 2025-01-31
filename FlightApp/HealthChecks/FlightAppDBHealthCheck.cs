using FlightApp.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FlightApp.HealthChecks
{
    public class FlightAppDBHealthCheck : IHealthCheck
    {
        private readonly FlightAppDbContext _dbContext;

        public FlightAppDBHealthCheck(FlightAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (await _dbContext.Database.CanConnectAsync(cancellationToken)) return HealthCheckResult.Healthy("FlightApp DB is healthy.");
            return HealthCheckResult.Unhealthy("FlightApp DB is unhealthy.");
        }
    }
}

using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace FlightApp.HealthChecks
{
    public class MapDataHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping sender = new Ping();
            PingReply reply = sender.Send("maps.geoapify.com");
            return reply.Status == IPStatus.Success
                ? Task.FromResult(HealthCheckResult.Healthy("Connection to Geoapify successful"))
                : Task.FromResult(HealthCheckResult.Unhealthy("Unable to connect to Geoapify"));
        }
    }
}

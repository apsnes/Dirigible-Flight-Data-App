using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace FlightApp.HealthChecks
{
    public class PlaneSpottersHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping sender = new Ping();
            PingReply reply = sender.Send("api.planespotters.net");
            return reply.Status == IPStatus.Success
                ? Task.FromResult(HealthCheckResult.Healthy("Connection to PlaneSpotters successful"))
                : Task.FromResult(HealthCheckResult.Unhealthy("Unable to connect to PlaneSpotters"));
        }
    }
}
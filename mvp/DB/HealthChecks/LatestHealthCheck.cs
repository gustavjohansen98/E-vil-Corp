using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Server
{
    class LatestHealthCheck : IHealthCheck
    {
        private static latest_global latest_;

        public LatestHealthCheck(latest_global LATEST) 
        {
            latest_ = LATEST;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (latest_.LATEST <= 0)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("LATEST was 0 or below, service is not running"));
            }

            return Task.FromResult(HealthCheckResult.Healthy("All good"));
        }
    }
}
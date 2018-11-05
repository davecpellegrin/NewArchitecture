using Microsoft.Extensions.DependencyInjection;
using SandboxCustomers.Api.Services;

namespace SandboxCustomers.Api
{
    public partial class Startup
    {
        private void ConfigureServices_DependencyInjection(IServiceCollection services)
        {
            // Singleton uses here so test repository will persist in the app (don't do ordinarily)
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
        }

    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SandboxCustomers.Api
{
    public partial class Startup
    {
        private void ConfigureServices_CORS(IServiceCollection services)
        {
            services.AddCors();
        }

        private void Configure_CORS(IApplicationBuilder app)
        {
            // define the client domains that can access this api
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        }
    }
}

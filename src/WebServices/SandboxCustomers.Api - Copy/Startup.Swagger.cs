using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace SandboxCustomers.Api
{
    public partial class Startup
    {
        private void ConfigureServices_Swagger(IServiceCollection services)
        {
            if (_enableApiVersioning)
            { // enable swagger services for api versioning
                services.AddSwaggerGen(options =>
                {
                    var apiVersionDescriptionProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName,
                            new Info() { Title = $"{ApplicationName} {description.ApiVersion}", Version = description.ApiVersion.ToString() });
                    }
                });
            }
            else
            { // _enableApiVersioning == false =>  normal swagger services with versioning
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", 
                        new Info { Title = $"{ApplicationName} Version 1", Version = "v1" });
                });
            }

        }

        private void Configure_Swagger(IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescProvider = null)
        {
            app.UseSwagger();

            if (_enableApiVersioning)
            { // enable swagger for api versioning
                app.UseSwaggerUI(options =>
                    {
                        foreach (var description in apiVersionDescProvider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint(
                                $"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                        }
                    });
            }
            else
            { // _enableApiVersioning == false =>  normal swagger services without versioning
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", ApplicationName);
                });
            }


        }

    }
}

// use "#define ENABLEAPIVERSIONING" (enabled) or "#undef ENABLEAPIVERSIONING" (disabled) to toggle versioning
// If enabled, remember to use this attributes for your controller classes:
//[ApiVersion("1.0")]
//[Route("api/v{version:apiVersion}/[controller]")]
#undef ENABLEAPIVERSIONING

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SandboxCustomers.Api
{
    public partial class Startup
    {
        // parameters for Startup
        public const string ApplicationName = "SandboxCustomers.Api";

        // global variables
        public IConfiguration Configuration { get; }
        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

// this will enable versioning
#if ENABLEAPIVERSIONING
            _enableApiVersioning = true;
#endif
        }


        // use #define or #undef for ENABLEAPIVERSIONING to set this properly
        private readonly bool _enableApiVersioning = false;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServices_DependencyInjection(services);

            if (_enableApiVersioning)
            {
                ConfigureServices_ApiVersioning(services);
            }
            
            ConfigureServices_CORS(services);
            ConfigureServices_Swagger(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if !ENABLEAPIVERSIONING
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider apiVersionDescProvider = null)
#endif
        
        {


#if !ENABLEAPIVERSIONING
            IApiVersionDescriptionProvider apiVersionDescProvider = null;
#endif


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            Configure_CORS(app);
            Configure_AutoMapper();
            Configure_Swagger(app, apiVersionDescProvider);

            app.UseMvc();
        }

    }

}

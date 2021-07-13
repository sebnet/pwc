using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OdenaPWC.Common;
using OdenaPWC.Repository;
using OdenaPWC.Service;
using OdenaPWC.TransportApiProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueCliMiddleware;

namespace OdenaPWC.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddProxies();
            services.AddServices();
            services.AddRepositories();

            var config = new ApiConfiguration();
            Configuration.Bind("TransportAPI", config);
            services.AddSingleton(config);

            services.AddDbContext<TransportContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TransportDatabase")));
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp/";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }

            });

            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<TransportContext>();
            context.Database.Migrate();

            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
        }
    }
}

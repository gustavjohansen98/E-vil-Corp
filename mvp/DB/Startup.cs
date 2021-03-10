using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Minitwit.Entities;
using Prometheus;
using Repos;

namespace Server
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
            Action<latest_global> latest_options = (opt => 
            {
                opt.LATEST = 0;
            });

            services.Configure(latest_options);
            services.AddSingleton(resolver =>
            {
                return resolver.GetRequiredService<IOptions<latest_global>>().Value;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1" });
            });

            services.AddDbContext<IMinitwitContext, MinitwitContext>(o => o.UseSqlite("Filename=../../../minitwit.db"));
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFollowerRepository, FollowerRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddControllers();

            // services.AddHealthChecks().ForwardToPrometheus();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1"));
            }

            // app.UseHttpsRedirection();   // this causes an SSL error when running againts the simulator ..


            app.UseRouting();
            
            app.UseRequestMiddleware(); // Prometheus

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapMetrics(); // Prometheus
            });

        }
    }
}

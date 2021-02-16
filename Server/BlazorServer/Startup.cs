using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorServer.Data;
using Microsoft.EntityFrameworkCore;
using Minitwit.Entities;
using Microsoft.Data.Sqlite;
using Repos;

namespace BlazorServer
{
    public class Startup
    {
        // private readonly string _corsSecret = "_corsSecret";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            // services.AddSingleton<WeatherForecastService>();

            // services.AddCors(options => 
            // {
            //     options.AddPolicy(name: _corsSecret,
            //         builder => 
            //         {
            //             builder.WithOrigins("http://localhost:5010", "http://localhost:5000")
            //             .AllowAnyMethod()
            //             .AllowAnyHeader();
            //         });
            // });

            services.AddDbContext<IMinitwitContext, MinitwitContext>(options => 
            {
                var _connection = new SqliteConnection(@"Data Source=\tmp\minitwit.db");
                _connection.Open();
                options.UseSqlite(_connection);
            });
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddSignalR();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

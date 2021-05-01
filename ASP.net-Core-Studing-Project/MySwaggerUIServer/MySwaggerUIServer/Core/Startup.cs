using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySwaggerUIServer.Exceptions;
using MySwaggerUIServer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MySwaggerUIServer.Core
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
            services.AddSingleton<IUsers, UsersInMemoryRepository>();


            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MySwaggerUIServer", Version = "v1" });
            })
                .ConfigureSwaggerGen(options => {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, "MySwaggerUIServer.xml");
                    options.IncludeXmlComments(xmlPath, true);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.ConfigObject = new Swashbuckle.AspNetCore.SwaggerUI.ConfigObject
                {
                    ShowCommonExtensions = true
                };
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication v1");

            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<UserExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}

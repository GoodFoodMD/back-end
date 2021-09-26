using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApplication1.DataAccess;

namespace WebApplication1
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
            services.AddDbContext<GoodFoodDbContext>();
            services.AddSwaggerGen(c =>  
            {  
                c.SwaggerDoc("v1", new OpenApiInfo  
                {  
                    Version = "v1",  
                    Title = "My Rijsat API",  
                    Description = "Rijsat ASP.NET Core Web API",  
                    TermsOfService = new Uri("https://rijsat.com/terms"),  
                    Contact = new OpenApiContact  
                    {  
                        Name = "Rijwan Ansari",  
                        Email = string.Empty,  
                        Url = new Uri("https://rijsat.com/spboyer"),  
                    },  
                    License = new OpenApiLicense  
                    {  
                        Name = "Use under Open Source",  
                        Url = new Uri("https://rijsat.com/license"),  
                    }  
                });  
            });  
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAllOriginsPolicy", // I introduced a string constant just as a label "AllowAllOriginsPolicy"
                    builder =>
                    {
                        builder
                            .AllowCredentials()
                            .WithOrigins(
                                "http://localhost:4200")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors("AllowAllOriginsPolicy");
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "GoodFood api");
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
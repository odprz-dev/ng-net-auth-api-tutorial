using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_resource_server.Utils.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace api_resource_server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        const string AngularClient = "angular-client";
        const string ReadPermissions = "read:permissions";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AngularClient, builder =>
                {
                    builder.WithOrigins("https://localhost:4200", "http://localhost:4200");
                    builder.WithHeaders("authorization");

                });
               
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration.GetSection("auth:authority").Value;
                options.Audience = Configuration.GetSection("auth:audience").Value;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    name: ReadPermissions,
                    policy => policy.Requirements.Add(new HasScopeRequirement(
                        ReadPermissions, Configuration.GetSection("auth:authority").Value))
                    );
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api_resource_server", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api_resource_server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AngularClient);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

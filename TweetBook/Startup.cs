using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TweetBook.Installers;
using System.Linq;
using System;
using TweetBook.Data;
using TweetBook.Services;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace TweetBook
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
            services.InstallServiceAssembly(Configuration);
            services.AddAutoMapper(typeof(Startup));
     
        } 
         
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "TweetBook v1"));
                  c.SwaggerEndpoint("./v1/swagger.json", "My TweetBook V1")) ; //originally "./swagger/v1/swagger.json"
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
                       
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHealthChecks("/");
        }
    }
}

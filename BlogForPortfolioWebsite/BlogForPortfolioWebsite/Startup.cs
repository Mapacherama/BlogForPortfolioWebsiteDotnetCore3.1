using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogForPortfolioWebsite.Data;
using BlogForPortfolioWebsite.Data.FileManager;
using BlogForPortfolioWebsite.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogForPortfolioWebsite
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(_configuration.GetConnectionString("DefaultConnection"),
                    MySqlServerVersion.LatestSupportedServerVersion,
                    optionsMysql => optionsMysql.EnableRetryOnFailure()));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })   
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            
            services.ConfigureApplicationCookie(options => { options.LoginPath = "/Auth/Login"; });

            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IFileManager, FileManager>();

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            

            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseMvcWithDefaultRoute();
            
            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });*/
        }
    }
}
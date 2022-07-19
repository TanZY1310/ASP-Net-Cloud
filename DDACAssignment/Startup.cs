using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DDACAssignment.Data;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Handlers.AwsSdk;

namespace DDACAssignment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AWSXRayRecorder.InitializeInstance(configuration: Configuration);
            AWSSDKHandler.RegisterXRayForAllServices();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<DDACAssignmentContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DDACAssignment_NewContext")));

            services.AddDbContext<DDACAssignment_NewContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DDACAssignment_NewContext")));
            //options.UseSqlServer(Configuration.GetConnectionString("DDACAssignmentContextConnection")));
            //options.UseSqlServer(Configuration.GetConnectionString("TestRealDB")));

            //services.AddDbContext<DDACAssignmentContext>(options =>
            //        //options.UseSqlServer(Configuration.GetConnectionString("DDACAssignmentContext")));
            //        options.UseSqlServer(Configuration.GetConnectionString("DDACAssignmentContextConnection")));
            ////options.UseSqlServer(Configuration.GetConnectionString("TestRealDB")));

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();  //Direct people to the login pages
            app.UseAuthorization(); //Direct to the page to check the authorization
                                    //Must be in ascending order

            app.UseXRay("musicport-app");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");  //First location 
                endpoints.MapRazorPages();
            });
        }
    }
}

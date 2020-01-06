using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebUI.Data;
using WebUI.Data.DataModel;
using WebUI.Data.Services;
using WebUI.Data.Services.Interfaces;
using WebUI.Models;

namespace WebUI
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
            services.AddDbContext<CharityDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<CharityUser, IdentityRole>()
                .AddEntityFrameworkStores<CharityDbContext>();

            // Data access services
            services.AddScoped<IInstitutionsService, InstitutionsService>();
            services.AddScoped<IDonationsService, DonationsService>();
            services.AddScoped<IMessagesService, MessagesService>();

            // View models
            services.AddTransient<IndexViewModel>();
            services.AddTransient<WhoWeHelpViewModel>();
            services.AddTransient<StatsViewModel>();
            services.AddTransient<MessageData>();
            services.AddTransient<CollectionData>();
            services.AddTransient<DonationFormModel>();

            services.AddControllersWithViews();

            services.AddLocalization(options => options.ResourcesPath = "");
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = GetSupportedCultures();
            //CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            //CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(supportedCultures[0]),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider> { new CookieRequestCultureProvider() }
            }) ;

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IList<CultureInfo> GetSupportedCultures()
        {
            var result = new List<CultureInfo>();
            var cultureStrings = Configuration.GetSection("Settings:SupportedCultures").Get<List<string>>();
            foreach(var c in cultureStrings)
            {
                result.Add(new CultureInfo(c));
            }
            return result;
        }
    }
}

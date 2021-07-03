using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PracticeCore.Data;
using PracticeCore.Helpers;
using PracticeCore.Models;
using PracticeCore.Repository;
using PracticeCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore
{
    public class Startup
    {
        private readonly IConfiguration _conf;

        public Startup(IConfiguration conf )
        {
            _conf = conf;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(_conf.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            //configure identity 
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>().AddDefaultTokenProviders();
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
            //disable client side validation
            //.AddViewOptions(option=> 
            //{
            //    option.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            //configure password in identity
            services.Configure<IdentityOptions>(option=>
            {
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;

                option.SignIn.RequireConfirmedEmail = true;
                //if wrong password 5 times then he will blocked by 10 min
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                option.Lockout.MaxFailedAccessAttempts = 3;
            });
            //token timespan
            services.Configure<DataProtectionTokenProviderOptions>(option =>
            {
                option.TokenLifespan = TimeSpan.FromMinutes(10);
            });
            services.ConfigureApplicationCookie(cookie=>
            {
                cookie.LoginPath = "/Log-In";
            });
            services.Configure<SMTPConfig>(_conf.GetSection("SMTPConfig"));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            //app.Use(async(context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from first middleware   ");
            //    await next();
            //    await context.Response.WriteAsync("Hello from first middleware bla bla bla");
            //}
            //);
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from second middleware   ");
            //    await next();
            //}
            //);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.Map("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "AdminRoute",
                    pattern: "{ area: exists}/{ controller = Home}/{ action = Index}/{ id ?}"
                    );
                //endpoints.MapControllerRoute(
                //    name: "Default",
                //    pattern: "BookApp/{controller=Home}/{action=Index}/{id?}"
                //    );
                //custom route for a url
                //endpoints.MapControllerRoute(
                //    name: "AboutUs",
                //    pattern: "BookApp/About-Us",
                //    defaults: new {controllers="Home",Action= "AboutUs" }
                //    );
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.Map("/index", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World! from index");
            //    });
            //});
        }
    }
}

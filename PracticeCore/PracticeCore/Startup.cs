using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PracticeCore.Data;
using PracticeCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Server=.;Database=BookStore;Integrated Security=true;"));
            services.AddControllersWithViews();
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
            //disable client side validation
            //.AddViewOptions(option=> 
            //{
            //    option.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            services.AddScoped<BookRepository, BookRepository>();
            services.AddScoped<LanguageRepository, LanguageRepository>();
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

            app.UseEndpoints(endpoints =>
            {
                //endpoints.Map("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(
                //    name: "Default",
                //    pattern: "BookApp/{controller=Home}/{action=Index}/{id?}"
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

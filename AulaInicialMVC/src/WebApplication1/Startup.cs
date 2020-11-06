using DevOI.UI.WebModelo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevOI.UI.WebModelo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            services.AddDbContext<Contexto>(Option => {
                Option.UseSqlServer(Configuration.GetConnectionString("Contexto"));
            });

            services.AddControllersWithViews();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ConsumesAttribute("application/json"));
            }).AddNewtonsoftJson()
            .AddRazorRuntimeCompilation()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddTransient<IPedidoRepository, PedidoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapAreaControllerRoute(
                    name: "Mercado",
                    areaName: "Mercado",
                    pattern: "Mercado/{controller}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                      name: "area",
                      areaName: "area",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                 name: "default",
                 //areaName: "default",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}

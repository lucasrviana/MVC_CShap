using CoreIdentity.Data;
using CoreIdentity.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreIdentity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment host)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(host.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{host.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if(host.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<CoreIdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Conexao")));

            services.Configure<CookiePolicyOptions>(option =>
            {
                option.CheckConsentNeeded = context => true;
                option.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CoreIdentityContext>();

            services.AddAuthorization(option =>
            {
                option.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));
                option.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer")));
                option.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever")));
            });

            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();
            services.AddScoped(context => new LoggerFactory().CreateLogger("Teste"));

            services.AddMvc(option => option.Filters.Add(typeof(AuditoriaFilter)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Erro/500");
                app.UseStatusCodePagesWithRedirects("/Erro/{0}");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}

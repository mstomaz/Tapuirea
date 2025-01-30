using blog_rpg.Data;
using blog_rpg.Routing;
using blog_rpg.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using blog_rpg.Areas.Identity.Data;
using blog_rpg.Areas.Identity.Models;

namespace blog_rpg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });

            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<TaleService>();
            builder.Services.AddScoped<UserService>();

            builder.Services.AddDbContext<BlogContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("MySqlConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))
            ));
            builder.Services.AddDbContext<UserAuthContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("UserAuthContextConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("UserAuthContextConnection"))
            ));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<UserAuthContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.SignIn.RequireConfirmedEmail = true;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
                    seedingService.Seed();
                }
            }

            if (!app.Environment.IsDevelopment())
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

            app.MapHealthChecks("/healthz");

            app.MapControllerRoute(
                name: "taleRead",
                pattern: "{controller:slugify}/{action:slugify}/{view:slugify}.{id}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller:slugify=home}/{action:slugify=index}");

            app.Run();
        }
    }
}

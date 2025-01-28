using blog_rpg.Data;
using blog_rpg.Routing;
using blog_rpg.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace blog_rpg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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

            builder.Services.AddDefaultIdentity<IdentityUser>(
                options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UserAuthContext>();

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

            // Configure the HTTP request pipeline.
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
                pattern: "{controller:slugify=Tales}/{action:slugify=Read}/{view:slugify}.{id}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller:slugify=Home}/{action:slugify=Index}");

            app.Run();
        }
    }
}

using blog_rpg.Data;
using blog_rpg.Routing;
using blog_rpg.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

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

            app.UseAuthorization();

            app.MapHealthChecks("/healthz");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller:slugify=Home}/{action:slugify=Index}/{view:slugify?}");

            app.Run();
        }
    }
}

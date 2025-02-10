using blog_rpg.Areas.Identity.Data;
using blog_rpg.Areas.Identity.Models;
using blog_rpg.Data;
using blog_rpg.Routing;
using blog_rpg.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using blog_rpg.Localization;
using blog_rpg.ErrorDescribers.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace blog_rpg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

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

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new CultureInfo[]
                {
                    new("pt-BR"),
                    new("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture("pt-BR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            builder.Services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<UserAuthContext>()
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;

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

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });

            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<TaleService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserErrorDescriber>();

            builder.Services.Configure<MvcOptions>(options =>
            {
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
                    (fieldName) => $"O campo '{fieldName}' não pode ser vazio.");
            });

            builder.Services.AddHealthChecks();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();


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

            var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>()!.Value;
            app.UseRequestLocalization(locOptions);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHealthChecks("/healthz");

            app.MapControllerRoute(
                name: "taleRead",
                pattern: "{controller:slugify}/{action:slugify}/{title:slugify}.{id}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller:slugify=home}/{action:slugify=index}");

            app.Run();
        }
    }
}

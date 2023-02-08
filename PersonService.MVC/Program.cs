using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonService.MVC.Data;
using PesonService.DAL;

namespace PersonService.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var identityConnStr = builder.Configuration.GetConnectionString("IdentityDbConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDbConnection' not found.");

            builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(identityConnStr));

            var dataConnStr = builder.Configuration.GetConnectionString("DataDbConnection") ?? throw new InvalidOperationException("Connection string 'DataDbConnection' not found.");

            builder.Services.AddDbContext<PersonServiceDbContext>(options => options.UseSqlServer(dataConnStr));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityDbContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddDalServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using EmployeeDetails.Repository;
using EmployeeDetails.Service;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace EmployeeDetails
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Add session services before app build
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout as needed
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddDbContextPool<AppdbContextRepository>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDBConnection"))
            );

            builder.Services.AddScoped<IEmployeeService, SQLEmployeeService>();

            var app = builder.Build();

            // Use session after app is built
            app.UseSession();  // Make sure to use session middleware here

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

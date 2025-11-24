using Company_site.Domain.Entities;
using Company_Site.Application.Interfaces;
using Company_Site.Infrastructure.Data;
using Company_Site.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using Company_Site.Web.Middleware;

namespace Company_Site.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connStr = "Server=DESKTOP-J95NUIR;Database=Company_Site;Trusted_Connection=True;TrustServerCertificate=True;";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataBaseContext>
                (p => p.UseSqlServer(connStr));
            builder.Services.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<DataBaseContext>()
               .AddDefaultTokenProviders();
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                connectionString: connStr,
                sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions 
                {
                    TableName="LogEvents",
                    AutoCreateSqlTable =true 
                }
                )
                .MinimumLevel.Error()
                .CreateLogger();
                

            var app = builder.Build();
           

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
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseMiddleware<ExceptionLoggingMiddleware>();

            app.Run();
        }
    }
}

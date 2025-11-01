using Company_site.Domain.Entities;
using Company_Site.Application.Interfaces;
using Company_Site.Infrastructure.Data;
using Company_Site.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company_Site.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataBaseContext>
                (p => p.UseSqlServer("Server=DESKTOP-J95NUIR;Database=Company_Site;Trusted_Connection=True;TrustServerCertificate=True;"));
            builder.Services.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<DataBaseContext>()
               .AddDefaultTokenProviders();

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

            app.Run();
        }
    }
}

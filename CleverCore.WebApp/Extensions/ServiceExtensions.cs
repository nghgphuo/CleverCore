using CleverCore.Application.AutoMapper;
using CleverCore.Application.Implementation;
using CleverCore.Application.Interfaces;
using CleverCore.Data.EF;
using CleverCore.Data.EF.Repositories;
using CleverCore.Data.Entities;
using CleverCore.Data.IRepositories;
using CleverCore.Infrastructure.Interfaces;
using CleverCore.WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleverCore.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly("CleverCore.Data.EF")));

            // Add Identity
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.User.RequireUniqueEmail = true;
            });

            // Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

            // Add application services
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<DbInitializer>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            services.AddControllersWithViews();

            return services;
        }
    }
}

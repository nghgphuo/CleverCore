using CleverCore.Data.EF;
using CleverCore.Data.Entities;
using CleverCore.WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CleverCore.Application.Interfaces;
using CleverCore.Data.EF.Repositories;
using CleverCore.Data.IRepositories;
using CleverCore.Application.Implementation;
using CleverCore.Application.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly("CleverCore.Data.EF")));

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add application builder.Services.
builder.Services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<DbInitializer>();
builder.Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddTransient<IProductCategoryService, ProductCategoryService>();

builder.Services.AddControllersWithViews();

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
app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    seeder.Seed();
}

app.Run();
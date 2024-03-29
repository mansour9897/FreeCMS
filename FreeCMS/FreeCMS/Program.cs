using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FreeCMS.Extensions;
using FreeCMS.DomainModels.Identity;
using FreeCMS.DAL;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using FreeCMS;
using FreeCMS.Service.System.Abstraction;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FreeCMSContextConnection") ?? throw new InvalidOperationException("Connection string 'FreeCMSContextConnection' not found.");

builder.Services.AddDbContext<
    FreeCMSContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<Role>()
    .AddErrorDescriber<PersianIdentityErrorDescriber>()
    .AddEntityFrameworkStores<FreeCMSContext>();

//builder.Services.AddIdentity<ApplicationUser,Role>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRol

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ConfigFreeCmsServices();
//builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddAutoMapper(typeof(Program));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

//var ctrl = FreeCmsBootstrapper.GetAllPermissions();

var sc = app.Services.CreateScope();
IDbInitializer dbInitializer = sc.ServiceProvider.GetRequiredService<IDbInitializer>();
dbInitializer.InitializeAsync();
FreeCmsBootstrapper.AddPermissions(sc.ServiceProvider.GetRequiredService<IPermissionService>());
FreeCmsBootstrapper.AddSelectLists(sc.ServiceProvider.GetRequiredService<ISelectListService>());
FreeCmsBootstrapper.AddSiteSettings(sc.ServiceProvider.GetRequiredService<ISettingService>());
app.Run();



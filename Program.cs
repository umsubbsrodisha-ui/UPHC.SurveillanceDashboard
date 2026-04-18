
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
////using UPHC.SurveillanceDashboard.Data;
////using UPHC.SurveillanceDashboard.Hubs;
////using UPHC.SurveillanceDashboard.Services;
//using UPHC.SurveillanceDashboard.Models;


//using UPHC.SurveillanceDashboard.Components;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error", createScopeForErrors: true);
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
//app.UseHttpsRedirection();

//app.UseAntiforgery();

//app.MapStaticAssets();
//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode();

//app.Run();


//-----------------------updated---------------

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UPHC.SurveillanceDashboard.Data;
using UPHC.SurveillanceDashboard.Hubs;
using UPHC.SurveillanceDashboard.Models;
using UPHC.SurveillanceDashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


// PostgreSQL 13 + Npgsql 10.0.1
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//In case of sql server
//builder.Services.AddDbContextFactory<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity with Roles with Postgres
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

//builder.Services.AddDefaultIdentity<IdentityUser>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 6;
//})
//.AddRoles<IdentityRole>()
//.AddEntityFrameworkStores<AppDbContext>();

// SignalR + Services
builder.Services.AddSignalR();
builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();
app.MapHub<NotificationHub>("/notificationHub");
app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // 1. Ensure Roles exist
    string[] roles = { "Admin", "Analyst", "UPHCUser", "CHCUser" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // 2. Seed Admin User
    string adminEmail = "admin";
    string adminPassword = "Admin@123"; // change in production!

    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var user = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            UPHCId = null // Admin is not tied to UPHC
        };

        var result = await userManager.CreateAsync(user, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}

// SEED DATABASE + ROLES (runs once)

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    //var dbFactory = services.GetRequiredService<IDbContextFactory<AppDbContext>>();

//    //using var context = dbFactory.CreateDbContext();


//    //// Seed Roles
//    string[] roleNames = { "UPHCUser", "Analyst","Admin","CHCUser" };
//    //foreach (var roleName in roleNames)
//    //{
//    //    if (!await context.Roles.AnyAsync(r => r.Name == roleName))
//    //    {
//    //        await services.GetRequiredService<RoleManager<IdentityRole>>().CreateAsync(new IdentityRole(roleName));
//    //    }
//    //}

//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//    foreach (var roleName in roleNames)
//    {
//        if (!await roleManager.RoleExistsAsync(roleName))
//        {
//            await roleManager.CreateAsync(new IdentityRole(roleName));
//        }
//    }
//}

app.Run();
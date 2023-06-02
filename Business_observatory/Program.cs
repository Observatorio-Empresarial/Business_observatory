using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Business_observatory.Data;
using Business_observatory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Amazon;

var builder = WebApplication.CreateBuilder(args);

//var awsOptions = builder.Configuration.GetAWSOptions();
//awsOptions.Region = RegionEndpoint.SAEast1;
//builder.Services.AddDefaultAWSOptions(awsOptions);
//builder.Services.AddAWSService<IAmazonS3>();


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//builder.Services.AddControllers().AddNewtonsoftJson();

//builder.Services.AddIdentity<ApplicationUser,ApplicationRole>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = true;
//})
//    .AddEntityFrameworkStores<ApplicationDbContext>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
//{
//    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

//    if (!roleManager.RoleExistsAsync("Admin").Result)
//    {
//        var role = new IdentityRole("Admin");
//        roleManager.CreateAsync(role).Wait();
//    }

//    if (!roleManager.RoleExistsAsync("Usuario").Result)
//    {
//        var role = new IdentityRole("Usuario");
//        roleManager.CreateAsync(role).Wait();
//    }

//    if (!roleManager.RoleExistsAsync("Empresa").Result)
//    {
//        var role = new IdentityRole("Empresa");
//        roleManager.CreateAsync(role).Wait();
//    }

//    if (!roleManager.RoleExistsAsync("Encargado").Result)
//    {
//        var role = new IdentityRole("Encargado");
//        roleManager.CreateAsync(role).Wait();
//    }
//}

app.Run();

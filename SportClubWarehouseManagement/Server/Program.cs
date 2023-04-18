using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using SportClubAPI.Data.Repositories;
using SportClubAPI.Data.Repositories.Interfaces;
using SportClubWarehouseManagement.Server.Data;
using SportClubWarehouseManagement.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddIdentityServer()
	.AddApiAuthorization<ApplicationUser, AppDbContext>();

builder.Services.AddAuthentication()
	.AddIdentityServerJwt();

builder.Services.Configure<IdentityOptions>(opt =>
{
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequireUppercase = false;
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISportGoodRepository, SportGoodRepository>();
builder.Services.AddScoped<ICustomerSportGoodRepository, CustomerSportGoodRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

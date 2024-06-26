using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StringDivideWebApp.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddDbContext<ApplicationDbContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<StringDivideIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StringDivideIdentityContextConnection")));

builder.Services.AddDefaultIdentity<StringDivideWebAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<StringDivideIdentityContext>();

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

app.UseSession();
app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

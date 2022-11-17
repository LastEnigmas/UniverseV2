using Data.MyDbCon;
using Microsoft.EntityFrameworkCore;
using CoreA.Services.MainService;
using static CoreA.Generator.ViewToString;
using Microsoft.AspNetCore.Authentication.Cookies;
using Data.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDb>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DbString")
    ));

builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddScoped<IViewRenderService, RenderViewToString>();

#region Security
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/SignIn";
    options.LogoutPath = "/SignOut";
    options.ExpireTimeSpan = TimeSpan.FromDays(29);
});
#endregion

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
    pattern: "{area=Main}/{controller=MainHome}/{action=SignIn}/{id?}");

app.Run();

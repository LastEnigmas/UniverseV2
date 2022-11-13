using Data.MyDbCon;
using Microsoft.EntityFrameworkCore;
using CoreA.Services.MainService;
using static CoreA.Generator.ViewToString;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDb>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DbString")
    ));

builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddScoped<IViewRenderService, RenderViewToString>();

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
    pattern: "{area=Main}/{controller=MainHome}/{action=SignIn}/{id?}");

app.Run();

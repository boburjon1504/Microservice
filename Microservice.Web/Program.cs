using Microservice.Web.Services;
using Microservice.Web.Services.Interfaces;
using Microservice.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

SD.CouponApiBase = builder.Configuration["ServiceUrls:CouponApi"];

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("CouponApi", (sp, client) =>
{
    client.BaseAddress = new Uri(SD.CouponApiBase);
});

builder.Services.AddScoped<ICouponService, CouponService>();

builder.Services.AddScoped<IBaseService, BaseService>();

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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

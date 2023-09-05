using EntityLayer.Concrete;
using HotelAndTours.DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>(x => { x.Password.RequireNonAlphanumeric = true; })
    .AddEntityFrameworkStores<Context>();
// Configure authentication and authorization middleware.
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Admin/Default/AccessDenied";
    options.LoginPath = "/Admin/Login/Index";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
});

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddHttpClient();
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"

);
// Default route for the Frontend area
app.MapControllerRoute(
    name: "frontend",
    pattern: "Frontend/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Frontend}/{controller=Home}/{action=Index}/{id?}");

app.Run();
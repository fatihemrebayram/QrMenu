using System.Text;
using HotelAndTours.DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QrMenu.BusinessLayer.Abstract;
using QrMenu.BusinessLayer.Concrete;
using QrMenu.DataAccessLayer.Abstract;
using QrMenu.DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>();

builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IMealDal, EfMealDal>();
builder.Services.AddScoped<IMealService,MealManager >();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = true;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = "https://localhost",
        ValidAudience = "https://localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hotelandtoursapiproject")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("QrMenuApiCors", opts =>
    {
        opts.WithOrigins("https://localhost:7236")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("QrMenuApiCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

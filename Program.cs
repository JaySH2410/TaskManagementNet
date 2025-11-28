using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskManagement.Data;
using TaskManagement.Repositories;
using TaskManagement.Services;
using TaskManagement.Middlewares;
using TaskManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

Log.Logger = new LoggerConfiguration()
       .WriteTo.Console()
       .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
       .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Use Serilog instead of built-in logging
builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnString")));

// Repositories & Services
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// USER & CLAIMS
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<ValidationFilter>();

// Controllers + Validation Filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ====================== JWT AUTH CONFIG ======================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();
// ======================= END JWT SETUP =======================

var app = builder.Build();

// PIPELINE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

//app.UseAuthentication();   
//app.UseAuthorization();

app.MapControllers();

app.Run();

//using Microsoft.EntityFrameworkCore;
//using Serilog;
//using TaskManagement.Data;
//using TaskManagement.Repositories;
//using TaskManagement.Services;
//using TaskManagement.Middlewares;
//using TaskManagement.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//Log.Logger = new LoggerConfiguration()
//       .WriteTo.Console()
//       .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
//       .CreateLogger();

//var builder = WebApplication.CreateBuilder(args);

//// Use Serilog instead of built-in logging
//builder.Host.UseSerilog();

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnString")));

//builder.Services.AddScoped<ValidationFilter>();

//builder.Services.AddScoped<ITaskRepository, TaskRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//builder.Services.AddScoped<ITaskService, TaskService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


//// Add services to the container.
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<ValidationFilter>();
//});

//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//        policy.AllowAnyHeader()
//              .AllowAnyMethod()
//              .AllowAnyOrigin());
//});

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.UseCors("AllowAll");

//app.MapControllers();

//app.Run();

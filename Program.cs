//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using Parliament_API.Data;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);


//// Add RazorpayService
//builder.Services.AddSingleton<Parliament_API.Services.RazorpayService>();

//// =====================
//// Services
//// =====================

//// Controllers
//builder.Services.AddControllers();

//// DbContext
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection"))
//);

//// Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Parliament API",
//        Version = "v1",
//        Description = "API to manage MPs and Videos"
//    });
//});

//// 🔥 CORS (Allow React)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("ReactApp", policy =>
//    {
//        policy
//            .WithOrigins("http://localhost:5173") // React dev server
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

//// JWT Authentication
//var key = Encoding.ASCII.GetBytes("VERY_SECRET_KEY_1234567890");
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});

//var app = builder.Build();

//// =====================
//// Middleware
//// =====================

//// Swagger
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parliament API V1");
//        c.RoutePrefix = "swagger";
//    });
//}

//app.UseStaticFiles();

//app.UseCors("ReactApp");

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();
////app.Urls.Add("http://0.0.0.0:5224");
////// Use Render's PORT environment variable or fallback to 5224 locally
////var port = Environment.GetEnvironmentVariable("PORT") ?? "5224";
////app.Urls.Add($"http://0.0.0.0:{port}");

//app.MapGet("/", context =>
//{
//    context.Response.Redirect("/swagger");
//    return Task.CompletedTask;
//});

//app.Run();










//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using Parliament_API.Data;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// =====================
//// Services
//// =====================

//// Add RazorpayService
//builder.Services.AddSingleton<Parliament_API.Services.RazorpayService>();

//// Controllers
//builder.Services.AddControllers();

//// DbContext
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection"))
//);

//// Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Parliament API",
//        Version = "v1",
//        Description = "API to manage MPs and Videos"
//    });
//});

//// CORS for React (optional)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("ReactApp", policy =>
//    {
//        policy
//            .WithOrigins("http://localhost:5173")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

//// JWT Authentication
//var key = Encoding.ASCII.GetBytes("VERY_SECRET_KEY_1234567890");
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});

//var app = builder.Build();

//// =====================
//// Middleware
//// =====================

//// Swagger
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parliament API V1");
//    c.RoutePrefix = "swagger";
//});

//app.UseStaticFiles();

//app.UseCors("ReactApp");

//// ⚠️ Disable HTTPS redirection in Docker
//if (!app.Environment.IsEnvironment("Docker"))
//{
//    app.UseHttpsRedirection();
//}

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//// Listen on all interfaces for Docker
//app.Urls.Add("http://0.0.0.0:5224");

//// Redirect root to Swagger
//app.MapGet("/", context =>
//{
//    context.Response.Redirect("/swagger");
//    return Task.CompletedTask;
//});

//app.Run();





//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using Parliament_API.Data;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// =====================
//// Services
//// =====================

//// RazorpayService
//builder.Services.AddSingleton<Parliament_API.Services.RazorpayService>();

//// Controllers
//builder.Services.AddControllers();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    // Use connection string from environment
//    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//    options.UseSqlServer(connectionString);
//});


//// Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Parliament API",
//        Version = "v1",
//        Description = "API to manage MPs and Videos"
//    });
//});

//// CORS for React
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("ReactApp", policy =>
//    {
//        policy.WithOrigins("http://localhost:5173")
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

//// JWT
//var key = Encoding.ASCII.GetBytes("VERY_SECRET_KEY_1234567890");
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});

//var app = builder.Build();

//// =====================
//// Middleware
//// =====================

//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parliament API V1");
//    c.RoutePrefix = "swagger";
//});

//app.UseStaticFiles();
//app.UseCors("ReactApp");

//// Only use HTTPS outside Docker
//if (!app.Environment.IsEnvironment("Docker"))
//{
//    app.UseHttpsRedirection();
//}

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//// Explicitly tell Kestrel to listen inside Docker
//var url = "http://0.0.0.0:5224";
//app.Urls.Clear();
//app.Urls.Add(url);
//Console.WriteLine($"API listening on {url}");

//// Redirect root to Swagger
//app.MapGet("/", context =>
//{
//    context.Response.Redirect("/swagger");
//    return Task.CompletedTask;
//});

//app.Run();

////these are for starting the docker container
////docker rm -f parliament_api_container
////docker run -d -p 5224:5224 --name parliament_api_container --restart unless-stopped parliament_api









using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Parliament_API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =====================
// 🔥 PORT FIX FOR RENDER (VERY IMPORTANT)
// =====================
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// =====================
// Services
// =====================

// Razorpay
builder.Services.AddSingleton<Parliament_API.Services.RazorpayService>();

// Controllers
builder.Services.AddControllers();

// =====================
// DATABASE
// =====================
// ⚠️ Use ENV variable on Render
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =====================
// Swagger
// =====================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Parliament API",
        Version = "v1"
    });

    // JWT Support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// =====================
// 🔥 CORS (React + Android + Production)
// =====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// =====================
// JWT AUTH
// =====================
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY")
             ?? "VERY_SECRET_KEY_1234567890";

var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// =====================
// Middleware
// =====================

// Swagger (ENABLE FOR RENDER)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parliament API V1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowAll");

// ⚠️ REMOVE HTTPS redirection for Render
// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Redirect root to Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();

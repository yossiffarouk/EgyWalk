using AutoMapper;
using EgyWalk.Api.Data;
using EgyWalk.Api.Mapping;
using EgyWalk.Api.MiddelWares;
using EgyWalk.Api.Repositories.ImageRepository;
using EgyWalk.Api.Repositories.RegionRepositroy;
using EgyWalk.Api.Repositories.TokenRepository;
using EgyWalk.Api.Repositories.WalkRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Net.NetworkInformation;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Log.txt" , rollingInterval : RollingInterval.Hour)
    .MinimumLevel.Information()
    .CreateLogger();



builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Egy Walks API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddScoped<IWalkRepository , WalkRepo>();
builder.Services.AddScoped<IRegionRepo , RegionRepo>();
builder.Services.AddScoped<ITokenRepo , TokenRepo>();
builder.Services.AddScoped<IimageRepo , ImageLocalRepo>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddDbContext<EgyWalkDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("EgyWalkConnectionString")));

builder.Services.AddDbContext<EgyWalkAthuDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("EgyWalkAthuConnectionString")));









builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("EgyWalks")
    .AddEntityFrameworkStores<EgyWalkAthuDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 1;
});









// jwt auth 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    }

);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExpctionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();

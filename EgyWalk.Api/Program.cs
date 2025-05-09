using AutoMapper;
using EgyWalk.Api.Data;
using EgyWalk.Api.Mapping;
using EgyWalk.Api.Repositories.WalkRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWalkRepository , WalkRepo>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddDbContext<EgyWalkDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("EgyWalkConnectionString")));





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

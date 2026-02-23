using InterestService.Application.DTO;
using InterestService.Application.Interfaces;
using InterestService.Repository.Data;
using InterestService.Repository.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IEmi, EmiRepo>();

builder.Services.AddHttpClient<EmiClient>(client => {
  client.BaseAddress = new Uri(builder.Configuration["ManagerAddress"] ?? "http://localhost:5182");
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
  builder.Configuration.GetConnectionString("dbconn")
  ));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

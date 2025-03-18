using Infrastructure.Kafka;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration; // Add this line

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");
builder.Services.AddDbContext<KafkaDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddSingleton<KafkaProducer>(sp =>
    new KafkaProducer("localhost:9092"));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

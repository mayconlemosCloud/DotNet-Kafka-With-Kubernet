using Infrastructure.Kafka;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");
builder.Services.AddDbContext<KafkaDbContext>(options =>
    options.UseSqlite(connectionString));

var kafkaBootstrapServers = builder.Configuration.GetSection("Kafka").GetValue<string>("BootstrapServers");
builder.Services.AddSingleton<KafkaConsumer>(sp =>
{
    var serviceProvider = new ServiceCollection()
        .AddLogging(configure => configure.AddConsole())
        .BuildServiceProvider();

    var logger = serviceProvider.GetService<ILogger<KafkaConsumer>>();
    return new KafkaConsumer(kafkaBootstrapServers, "test-group", logger);
});

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

var kafkaConsumer = app.Services.GetRequiredService<KafkaConsumer>();
var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;

Task.Run(() => kafkaConsumer.Consume("test-topic", cancellationToken), cancellationToken);

app.Run();

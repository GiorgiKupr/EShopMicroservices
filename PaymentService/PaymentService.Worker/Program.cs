using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Http.Resilience;
using PaymentService.Worker;
using PaymentService.Worker.Data;
using PaymentService.Worker.IntegrationEvents.EventHandlers;
using PaymentService.Worker.Services;
using Polly;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<PaymentServiceDbContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddHttpClient<IPaymentGatewayClient, PaymentGatewayClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7081");
}).AddStandardResilienceHandler(options =>
{
    //overriding
    options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(2);
    options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(10);

    // Overriding Retry policy jitter enabled by default.
    options.Retry.MaxRetryAttempts = 3;
    options.Retry.BackoffType = DelayBackoffType.Exponential;
    options.Retry.Delay = TimeSpan.FromSeconds(2);

    // Overrding default values - Circuit breaker
    options.CircuitBreaker.FailureRatio = 0.5;
    options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
    options.CircuitBreaker.MinimumThroughput = 10;
    options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(30);
}); 


builder.Services.AddCap(x =>
{
    x.UseEntityFramework<PaymentServiceDbContext>();
    x.UseKafka("localhost:9092");
    x.UseDashboard();
});

builder.Services.AddTransient<StockReservedEventHandler>();


var host = builder.Build();
host.Run();

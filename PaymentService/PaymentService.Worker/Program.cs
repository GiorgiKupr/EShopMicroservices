using Microsoft.EntityFrameworkCore;
using PaymentService.Worker;
using PaymentService.Worker.Data;
using PaymentService.Worker.IntegrationEvents.EventHandlers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<PaymentServiceDbContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCap(x =>
{
    x.UseEntityFramework<PaymentServiceDbContext>();
    x.UseKafka("localhost:9092");
    x.UseDashboard();
});

builder.Services.AddTransient<StockReservedEventHandler>();


var host = builder.Build();
host.Run();

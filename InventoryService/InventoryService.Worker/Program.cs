using Consul;
using InventoryService.Worker;
using InventoryService.Worker.Data;
using InventoryService.Worker.IntegrationEvents.EventHandlers;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContext<InventoryServiceDbContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCap(x =>
{
    x.UseEntityFramework<InventoryServiceDbContext>();
    x.UseKafka("localhost:9092");
    x.UseDashboard();
});

builder.Services.AddTransient<OrderCreatedEventHandler>();
builder.Services.AddTransient<PaymentFailedEventHandler>();

var host = builder.Build();
host.Run();

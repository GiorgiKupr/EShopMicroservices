using DotNetCore.CAP;
using EventBus;
using InventoryService.Worker.Data;
using InventoryService.Worker.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Worker.IntegrationEvents.EventHandlers
{
    public class PaymentFailedEventHandler : ICapSubscribe
    {
        private readonly ICapPublisher _publisher;
        private readonly InventoryServiceDbContext _dbContext;

        public PaymentFailedEventHandler(ICapPublisher capPublisher, InventoryServiceDbContext dbContext)
        {
            _publisher = capPublisher;
            _dbContext = dbContext;
        }

        [CapSubscribe(TopicNames.PaymentFailed)]
        public async Task OrderCreatedConsumerAsync(PaymentFailedEvent @event)
        {
            var item = await _dbContext.inventories.Where(a => a.ProductId == @event.ProductId).FirstOrDefaultAsync();
            item.Stock += @event.TotalAmount;
            await _dbContext.SaveChangesAsync();
        }
    }
}

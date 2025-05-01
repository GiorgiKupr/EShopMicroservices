using DotNetCore.CAP;
using EventBus;
using InventoryService.Worker.Data;
using InventoryService.Worker.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Worker.IntegrationEvents.EventHandlers
{
    public class OrderCreatedEventHandler : ICapSubscribe
    {
        private readonly ICapPublisher _publisher;
        private readonly InventoryServiceDbContext _dbContext;

        public OrderCreatedEventHandler(ICapPublisher capPublisher, InventoryServiceDbContext dbContext)
        {
            _publisher = capPublisher;
            _dbContext = dbContext;
        }

        [CapSubscribe(TopicNames.OrderCreated)]
        public async Task OrderCreatedConsumerAsync(OrderCreatedEvent order)
        {
            var item = await _dbContext.inventories.Where(a => a.ProductId == order.ProductId).FirstOrDefaultAsync();

            bool stockResult = false;
            if (item.Stock > order.TotalAmount) stockResult = true;

            if (stockResult)
            {
                using (var transaction = _dbContext.Database.BeginTransactionAsync(_publisher, autoCommit: true))
                {
                    item.Stock -= order.TotalAmount;
                    _dbContext.inventories.Update(item);
                    await _publisher.PublishAsync(TopicNames.StockReserved, new StockReservedEvent(order.OrderId, order.CustomerId, item.Price, order.TotalAmount, item.ProductId));
                }
            }
            else
            {
                await _publisher.PublishAsync(TopicNames.StockNotReserved, new StockNotReservedEvent(order.OrderId, "there is no items in stock"));
            }
        }
    }
}

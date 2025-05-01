namespace InventoryService.Worker.IntegrationEvents.Events
{
    public record StockReservedEvent(Guid OrderId, Guid CustomerId, decimal Price, int TotalAmount, Guid ProductId);
}

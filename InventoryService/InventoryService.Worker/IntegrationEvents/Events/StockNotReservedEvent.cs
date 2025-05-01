namespace InventoryService.Worker.IntegrationEvents.Events
{
    public record StockNotReservedEvent(Guid OrderId, string error);
}

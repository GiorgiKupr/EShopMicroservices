namespace InventoryService.Worker.IntegrationEvents.Events
{
    public record PaymentFailedEvent(Guid OrderId, int TotalAmount, Guid ProductId);

}


namespace OrderService.Application.IntegrationEvents.Events
{
    public record StockNotReservedEvent(Guid OrderId, string error);


}

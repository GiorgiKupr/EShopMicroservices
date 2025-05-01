namespace PaymentService.Worker.IntegrationEvents.Events
{
    public record PaymentSuccesfulEvent(Guid OrderId);
}

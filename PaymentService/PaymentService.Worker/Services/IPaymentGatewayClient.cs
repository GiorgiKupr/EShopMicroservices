namespace PaymentService.Worker.Services
{
    public interface IPaymentGatewayClient
    {
        Task<string> ProcessPaymentAsync();
    }
}

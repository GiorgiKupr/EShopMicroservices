using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Worker.Services
{
    public class PaymentGatewayClient : IPaymentGatewayClient
    {
        private readonly HttpClient _httpClient;

        public PaymentGatewayClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ProcessPaymentAsync()
        {
            var response = await _httpClient.PostAsync("/api/payments", null);

            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }
            return "Failure";
        }
    }
}

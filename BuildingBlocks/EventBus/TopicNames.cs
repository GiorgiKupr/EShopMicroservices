using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public static class TopicNames
    {
        public const string OrderCreated = "Order.Created";
        public const string StockReserved = "Stock.Reserved";
        public const string StockNotReserved = "Stock.NotReserved";
        public const string PaymentSuccesful = "Payment.Successful";
        public const string PaymentFailed = "Payment.Failed";




    }
}

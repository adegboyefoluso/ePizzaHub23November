using ePizzaHub.Core.Entities;
using ePizzaHub.Services.Implementation;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interfaces
{
    public interface IPaymentService : IServices<PaymentDetail>
    {
        string CreateOrder(decimal amount, string currency, string receipt);
        Payment GetPaymentDetails(string paymentId);
        bool VerifySignature(string signature, string orderId, string paymentId);
        int SavePaymentDetails(PaymentDetail model);
    }

}

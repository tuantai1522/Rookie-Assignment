using Rookie.Mvc.ViewModels;

namespace Rookie.Mvc.Interface
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequest model);
        VnPaymentResponse PaymentExecute(IQueryCollection collection);
    }
}
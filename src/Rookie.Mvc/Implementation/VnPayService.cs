using Rookie.Mvc.Interface;
using Rookie.Mvc.Utils;
using Rookie.Mvc.ViewModels;

namespace Rookie.Mvc.Implementation
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration config;
        public VnPayService(IConfiguration config)
        {
            this.config = config;
        }
        public string CreatePaymentUrl(HttpContext context, VnPaymentRequest model)
        {
            var tick = DateTime.Now.Ticks.ToString();

            var vnpay = new VnPay();
            vnpay.AddRequestData("vnp_Version", config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.Total * 100).ToString());

            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utilities.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", config["VnPay:Locale"]);

            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán cho đơn hàng:" + model.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", config["VnPay:PaymentBackReturnUrl"]);

            vnpay.AddRequestData("vnp_TxnRef", tick); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            var paymentUrl = vnpay.CreateRequestUrl(config["VnPay:BaseUrl"], config["VnPay:HashSecret"]);

            return paymentUrl;
        }

        public VnPaymentResponse PaymentExecute(IQueryCollection collections)
        {
            var vnpay = new VnPay();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, config["VnPay:HashSecret"]);
            if (!checkSignature)
            {
                return new VnPaymentResponse
                {
                    Success = false
                };
            }

            return new VnPaymentResponse
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_orderId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode
            };
        }
    }
}
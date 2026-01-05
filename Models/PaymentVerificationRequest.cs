namespace Parliament_API.Models
{
    public class PaymentVerificationRequest
    {
        public string RazorpayOrderId { get; set; } = null!;
        public string RazorpayPaymentId { get; set; } = null!;
        public string RazorpaySignature { get; set; } = null!;
    }
}

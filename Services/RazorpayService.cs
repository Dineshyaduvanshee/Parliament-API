using Razorpay.Api;

namespace Parliament_API.Services
{
    public class RazorpayService
    {
        private readonly string _key;
        private readonly string _secret;

        public RazorpayService(IConfiguration config)
        {
            _key = config["Razorpay:Key"];
            _secret = config["Razorpay:Secret"];
        }

        public Order CreateOrder(decimal amount)
        {
            var client = new RazorpayClient(_key, _secret);

            Dictionary<string, object> options = new()
            {
                { "amount", (int)(amount * 100) }, // ₹ → paise
                { "currency", "INR" },
                { "payment_capture", 1 }
            };

            return client.Order.Create(options);
        }

        public string GetKey() => _key;
    }
}

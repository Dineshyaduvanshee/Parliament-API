using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parliament_API.Data;
using Parliament_API.Models;
using Parliament_API.Services;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly RazorpayService _razorpay;
        private readonly ApplicationDbContext _context;

        public PaymentController(RazorpayService razorpay, ApplicationDbContext context)
        {
            _razorpay = razorpay;
            _context = context;
        }

        // GET: api/payment/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _context.Payments
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(payments);
        }

        // POST: api/payment/create-order
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            if (request.Amount <= 0)
                return BadRequest("Invalid amount");

            var order = _razorpay.CreateOrder(request.Amount);

            var payment = new Payment
            {
                RazorpayOrderId = order["id"].ToString()!,
                Amount = request.Amount,
                Currency = order["currency"].ToString()!,
                Status = "created"
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = order["id"],
                amount = order["amount"],
                currency = order["currency"]
            });
        }

        // POST: api/payment/verify
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] PaymentVerificationRequest request)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(x => x.RazorpayOrderId == request.RazorpayOrderId);

            if (payment == null)
                return NotFound("Order not found");

            payment.RazorpayPaymentId = request.RazorpayPaymentId;
            payment.RazorpaySignature = request.RazorpaySignature;
            payment.Status = "paid";

            await _context.SaveChangesAsync();

            return Ok(new { message = "Payment verified & saved" });
        }
    }
}

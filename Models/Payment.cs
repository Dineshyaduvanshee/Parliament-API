using System;
using System.ComponentModel.DataAnnotations;

namespace Parliament_API.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RazorpayOrderId { get; set; } = null!;

        public string? RazorpayPaymentId { get; set; }

        public string? RazorpaySignature { get; set; }

        [Required]
        public decimal Amount { get; set; } // in ₹

        [Required]
        public string Currency { get; set; } = "INR";

        [Required]
        public string Status { get; set; } = "created"; // created, paid, failed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

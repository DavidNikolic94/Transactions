using System;

namespace Transactions.Models
{
    public class Transaction
    {
        public string? TransactionId { get; set; }
        public string? ExternalTransactionId { get; set; }
        public string? UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? Message { get; set; }
        public int Status { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Hash { get; set; }
    }
}

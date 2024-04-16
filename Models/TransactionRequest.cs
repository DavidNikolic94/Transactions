namespace Transactions.Models
{
    public class TransactionRequest
    {
        public string? ExternalTransactionId { get; set; }
        public string? UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
    }

}
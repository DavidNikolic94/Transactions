namespace Transactions.Models
{

    public class TransactionResponse
    {
        public string? TransactionId { get; set; }
        public string? Message { get; set; }
        public int Status { get; set; }
    }
}
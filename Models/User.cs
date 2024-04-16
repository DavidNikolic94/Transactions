namespace Transactions.Models
{
    public class User
    {
        public string? Id { get; set; }
        public decimal Balance { get; set; }

        public string? UserName { get; set; }
        public string? Currency { get; set; }
    }

}

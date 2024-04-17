using Transactions.Models;

namespace Transactions.Services
{
    public interface ITransactionService
    {
        Task<TransactionResponse> ProcessTransaction(TransactionRequest request, string FromUserId);

    }
}

namespace Transactions.Services
{
    using System;
    using Transactions.Data;
    using Transactions.Models;

    public class TransactionService : ITransactionService
    {
        private readonly TransactionDbContext _context;

        public TransactionService(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionResponse> ProcessTransaction(TransactionRequest request, string fromUserId)
        {
            var rcvUser = await _context.Users.FindAsync(request.UserId);
            var sndUser = await _context.Users.FindAsync(fromUserId);

            if (sndUser == null)
            {
                return new TransactionResponse
                {
                    Message = "Sender not found",
                    Status = 1
                };
            }

            if (rcvUser == null)
            {
                return new TransactionResponse
                {
                    Message = "Receiver not found",
                    Status = 1
                };
            }

            if (sndUser.Currency != request.Currency || rcvUser.Currency != request.Currency)
            {
                return new TransactionResponse
                {
                    Message = "Invalid currency",
                    Status = 2
                };
            }

            sndUser.Balance -= request.Amount;
            rcvUser.Balance += request.Amount;
            _context.Users.Update(sndUser);
            _context.Users.Update(rcvUser);
            await _context.SaveChangesAsync();

            return new TransactionResponse
            {
                TransactionId = Guid.NewGuid().ToString(),
                Message = "Transaction processed successfully",
                Status = 0
            };
        }
    }



}

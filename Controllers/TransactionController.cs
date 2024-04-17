using Microsoft.AspNetCore.Mvc;
using Transactions.Models;
using Transactions.Services;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TransactionRequest request, string fromUserId)
    {
        try
        {

            var response = await _transactionService.ProcessTransaction(request, fromUserId);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}

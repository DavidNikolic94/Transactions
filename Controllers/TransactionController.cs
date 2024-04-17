using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
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
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TransactionRequest request, string fromUserId)
    {
        try
        {
            Request.Headers.TryGetValue("hash", out var requestHash);

            var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
            var computedHash = await ComputeHashAsync(request, secretKey);

            if (String.IsNullOrEmpty(requestHash))
            {
                return BadRequest("Missing or empty hash header.");
            }

            if (requestHash != computedHash)
            {
                return BadRequest("Invalid hash.");
            }

            var response = await _transactionService.ProcessTransaction(request, fromUserId);

            _logger.LogInformation("Request: {@Request}", request);
            _logger.LogInformation("Response: {@Response}", response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing transaction");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    private Task<string> ComputeHashAsync(TransactionRequest request, string secretKey)
    {
        return Task.Run(() =>
        {
            var payload = $"{request.ExternalTransactionId}{request.UserId}{request.Amount}{request.Currency}{secretKey}";
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        });
    }
}

using AceInternBatch1DotNetCore.MiniKpayWebApi.Models;
using AceInternBatch1DotNetCore.MiniKpayWebApi.Models.TransactionHistory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AceInternBatch1DotNetCore.MiniKpayWebApi.Features.TransactionHistory
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly TransactionHistoryBL _transactionHistoryBL;

        public TransactionHistoryController(TransactionHistoryBL transactionHistoryBL)
        {
            _transactionHistoryBL = transactionHistoryBL;
        }

        [HttpPost]
        public async Task<IActionResult> TransactionHistory(TransactionHistoryRequestModel requestModel)
        {
            try
            {
                var result = requestModel.IsValid();
                if (!result.Success)
                    return BadRequest(result);

                result = await _transactionHistoryBL.TransactionHistory(requestModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = Result<TransactionHistoryResponseModel>.FailureResult(ex);
                return StatusCode(500, result);
            }
        }

        [HttpPost("Datetime")]
        public async Task<IActionResult> TransactionDateHistory(TransactionHistoryDateRequestModel requestModel)
        {
            try
            {
                var result = requestModel.IsTransactionDateValid();
                if (!result.Success)
                    return BadRequest(result);

                result = await _transactionHistoryBL.TransactionDateHistory(requestModel);

                return Ok(result);

            }
            catch (Exception ex)
            {
                var result = Result<TransactionHistoryResponseModel>.FailureResult(ex);
                return StatusCode(500, result);
            }
        }
    }
}

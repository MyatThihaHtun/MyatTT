using AceInternBatch1DotNetCore.MiniKpayWebApi.Models.Transfer;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using AceInternBatch1DotNetCore.MiniKpayWebApi.Models;

namespace AceInternBatch1DotNetCore.MiniKpayWebApi.Features.Transfer
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly TransferBL _transferBL;

        public TransferController(TransferBL transferBl)
        {
            _transferBL = transferBl;
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferRequestModel requestModel)
        {
            try
            {
                var result = requestModel.IsValid();
                if (!result.Success)
                {
                    return BadRequest(result);
                }

                result = await _transferBL.Transfer(requestModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var model = Result<TransferResponseModel>.FailureResult(ex.ToString());
                return StatusCode(500, model);
            }
        }
    }
}

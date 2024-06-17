using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AceInternBatch1DotNetCore.MiniKpayWebApi.Features.TransactionHistory
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        [HttpPost]
        public IActionResult TransactionHistory(TransactionHistoryRequestModel requestModel)
        {
            try
            {
                if(string.IsNullOrEmpty(requestModel.CustomerCode))
                {
                    return BadRequest("Invalid Customer Code.")
                }

                return Ok();
            }
            catch (Exception ex)  
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}

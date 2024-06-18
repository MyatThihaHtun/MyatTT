using AceInternBatch1DotNetCore.MiniKpayWebApi.Models.Transfer;
using AceInternBatch1DotNetCore.MiniKpayWebApi.Models;

namespace AceInternBatch1DotNetCore.MiniKpayWebApi.Features.Transfer
{
    public class TransferBL
    {
        private readonly TransferDA _transferDA;

        public TransferBL(TransferDA transferDA)
        {
            _transferDA = transferDA;
        }

        public async Task<Result<TransferResponseModel>> Transfer(TransferRequestModel requestModel)
        {
            var result = await _transferDA.ValidateMobileNumber(requestModel.FromMobileNo);
            if (!result.Success)
                return result;

            result = await _transferDA.ValidateMobileNumber(requestModel.ToMobileNo);
            if (!result.Success)
                return result;

            result = await _transferDA.HandleInsufficientBalance(requestModel.FromMobileNo, requestModel.Amount);
            if (!result.Success)
                return result;

            await _transferDA.DecreaseAmount(requestModel.FromMobileNo, requestModel.Amount);
            await _transferDA.IncreaseAmount(requestModel.ToMobileNo, requestModel.Amount);
            await _transferDA.CreateTransaction(requestModel);

            return Result<TransferResponseModel>.SuccessResult();
        }
    }
}

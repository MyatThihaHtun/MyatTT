using AceInternBatch1DotNetCore.MiniKpayWebApi.Models;
using AceInternBatch1DotNetCore.MiniKpayWebApi.Models.TransactionHistory;

namespace AceInternBatch1DotNetCore.MiniKpayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryBL
    {
        private readonly TransactionHistoryDA _transactionHistoryDA;

        public TransactionHistoryBL(TransactionHistoryDA transactionHistoryDA)
        {
            _transactionHistoryDA = transactionHistoryDA;
        }

        public async Task<Result<TransactionHistoryResponseModel>> TransactionHistory(
            TransactionHistoryRequestModel requestModel)
        {
            bool isExist = await _transactionHistoryDA.IsExistCustomerCode(requestModel.CustomerCode!);
            if (!isExist)
                return Result<TransactionHistoryResponseModel>.FailureResult("Customer doesn't exist.");

            var lst = await _transactionHistoryDA.TransactionHistoryByCustomerCode(requestModel.CustomerCode!);
            var model = new TransactionHistoryResponseModel()
            {
                Data = lst
            };
            return Result<TransactionHistoryResponseModel>.SuccessResult(model);
        }

        public async Task<Result<TransactionHistoryResponseModel>> TransactionDateHistory(
            TransactionHistoryDateRequestModel requestModel)
        {
            bool isExit = await _transactionHistoryDA.IsExistTransactionDate(requestModel.TransactionDate);
            if (!isExit)
                return Result<TransactionHistoryResponseModel>.FailureResult("History Date doesn't exist.");

            var lst = await _transactionHistoryDA.TransactionHistoryByDatetime(requestModel.TransactionDate);
            var model = new TransactionHistoryResponseModel()
            {
                Data = lst
            };
            return Result<TransactionHistoryResponseModel>.SuccessResult(model);
        }
    }
}

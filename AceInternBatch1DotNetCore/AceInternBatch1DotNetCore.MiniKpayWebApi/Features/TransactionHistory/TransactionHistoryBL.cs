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

        public TransactionHistoryResponseModel TransactionHistory(TransactionHistoryRequestModel requestModel)
        {
            TransactionHistoryResponseModel model = new TransactionHistoryResponseModel();

            bool isExist = _transactionHistoryDA.IsExistCustomerCode(requestModel.CustomerCode!);
            if (!isExist)
            {
                model.IsSuccess = false;
                model.Message = "Customer doesn't exist.";
                return model;
            }


            var lst = _transactionHistoryDA.TransactionHistoryByCustomerCode(requestModel.CustomerCode!);
            model.IsSuccess = true;
            model.Message = "Success.";
            model.Data = lst;
            return model;
        }
    }
}

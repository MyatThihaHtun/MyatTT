using AceInternBatch1DotNetCore.MiniKpayWebApi.Models.Transfer;

namespace AceInternBatch1DotNetCore.MiniKpayWebApi.Models.TransactionHistory
{
    public class TransactionHistoryResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<CustomerTransactionHistoryModel> Data { get; set; }
    }
}

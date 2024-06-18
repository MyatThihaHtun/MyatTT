using AceInternBatch1DotNetCore.MiniKpayWebApi.Models.Transfer;
using System.Data;
using Dapper;
using AceInternBatch1DotNetCore.MiniKpayWebApi.Models;

namespace AceInternBatch1DotNetCore.MiniKpayWebApi.Features.Transfer
{
    public class TransferDA
    {
        private readonly IDbConnection _db;

        public TransferDA(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Result<TransferResponseModel>> HandleInsufficientBalance(string mobileNo, decimal amount)
        {
            string query = @"select cb.* from Tbl_Customer c
inner join Tbl_CustomerBalance cb on cb.CustomerCode = c.CustomerCode
where MobileNo = @MobileNo";
            var queryResult = await _db.QueryAsync<CustomerBalanceModel>(query, new { MobileNo = mobileNo });
            var item = queryResult.FirstOrDefault();
            if (item is null)
            {
                return Result<TransferResponseModel>.FailureResult("Invalid Mobile No.");
            }

            if (amount > item.Balance)
            {
                return Result<TransferResponseModel>.FailureResult("Insufficient Balance.");
            }

            return Result<TransferResponseModel>.SuccessResult();
        }

        public async Task<Result<TransferResponseModel>> ValidateMobileNumber(string mobileNo)
        {
            string query = "select * from Tbl_Customer with (nolock) where MobileNo = @MobileNo";
            var queryResult = await _db.QueryAsync<CustomerModel>(query, new { MobielNo = mobileNo });
            var item = queryResult.FirstOrDefault();
            if (item is null)
            {
                return Result<TransferResponseModel>.FailureResult("Invalid Mobile No.");
            }

            return Result<TransferResponseModel>.SuccessResult();
        }

        public async Task<Result<TransferResponseModel>> DecreaseAmount(string mobileNo, decimal amount)
        {
            string query = "Update Tbl_Customer Set Balance = Balance - @Balance where MobileNo = @MobileNo";
            var queryResult = await _db.ExecuteAsync(query, new { MobileNo = mobileNo, Balance = amount });

            return Result<TransferResponseModel>.SuccessResult();
        }

        public async Task<Result<TransferResponseModel>> IncreaseAmount(string mobileNo, decimal amount)
        {
            string query = "Update Tbl_Customer Set Balance = Balance + @Balance where MobileNo = @MobileNo";
            var queryResult = await _db.ExecuteAsync(query, new { MobileNo = mobileNo, Balance = amount });

            return Result<TransferResponseModel>.SuccessResult();
        }

        public async Task<Result<TransferResponseModel>> CreateTransaction(TransferRequestModel requestModel)
        {
            string query = @"insert into Tbl_CustomerTransactionHistory (
        FromMobileNo, ToMobileNo, Amount, TransactionDate)
values (@FromMobileNo, @ToMobileNo, @Amount, TransactionDate);";
            var queryResult = await _db.ExecuteAsync(query, new
            {
                FromMobileNo = requestModel.FromMobileNo,
                ToMobileNo = requestModel.ToMobileNo,
                Amount = requestModel.Amount,
                TransactionDate = DateTime.Now
            });

            return Result<TransferResponseModel>.SuccessResult();
        }
    }
}

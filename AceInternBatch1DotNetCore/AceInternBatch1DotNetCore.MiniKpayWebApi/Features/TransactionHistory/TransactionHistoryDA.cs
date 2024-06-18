using System.Data;
using Dapper;
using AceInternBatch1DotNetCore.MiniKpayWebApi.Models.Transfer;

namespace AceInternBatch1DotNetCore.MiniKpayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryDA
    {

        private readonly IDbConnection _db;

        public TransactionHistoryDA(IDbConnection db)
        {
            _db = db;
        }

        public bool IsExistCustomerCode(string customerCode)
        {
            string query = "select * from Tbl_Customer with (nolock) where CustomerCode = @CustomerCode";
            var item = _db.Query<CustomerModel>(query, new { CustomerCode = customerCode }).FirstOrDefault();
            // Transaction History By Customer Code
            //return item == null ? false : true;
            //return item != null ? true : false;
            //return item != null;
            return item is not null;
        }

        public List<CustomerTransactionHistoryModel> TransactionHistoryByCustomerCode(string customerCode)
        {
            var lst = new List<CustomerTransactionHistoryModel>();

            string query = @"select CTH.* from Tbl_CustomerTransactionHistory CTH
inner join Tbl_Customer C on CTH.FromMobileNo = C.MobileNo
where CustomerCode = @CustomerCode";

            lst = _db.Query<CustomerTransactionHistoryModel>(query, new { CustomerCode = customerCode }).ToList();

            return lst;
        }
       
    }
}

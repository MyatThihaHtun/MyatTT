﻿namespace AceInternBatch1DotNetCore.MiniKpayWebApi.Models
{
    public class CustomerTransactionHistoryModel
    {
        public int CustomerTransactionHistoryId { get; set; }
        public string FromMobileNo { get; set; }

        public string ToMobileNo { get; set; }

        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

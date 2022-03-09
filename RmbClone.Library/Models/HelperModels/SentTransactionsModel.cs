using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models.HelperModels
{
    public class SentTransactionsModel
    {
        public string Id { get; set; }
        public string SentTo { get; set; }
        public string ReceiverTransactionNumber { get; set; }
        public string TransactionReason { get; set; }
        public double SentAmount { get; set; }
        public DateTime DateOfSending { get; set; }
    }
}

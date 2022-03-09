using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models.HelperModels
{
    public class ReceivedTransactionsModel
    {
        public string Id { get; set; }
        public string ReceivedFrom { get; set; }
        public string SenderTransactionNumber { get; set; }
        public string TransactionReason { get; set; }
        public double ReceivedAmount { get; set; }
        public DateTime DateOfReceiving { get; set; }

    }
}

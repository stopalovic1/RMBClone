using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models.Requests
{
    public class TransactionRequestModel
    {

        public string SenderTransactionNumber { get; set; }
        public string ReceiverTransactionNumber { get; set; }
        public string TransactionReason { get; set; }
        public double TransactionValue { get; set; }

    }
}

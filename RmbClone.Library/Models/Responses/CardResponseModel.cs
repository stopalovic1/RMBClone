using RmbClone.Library.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models.Responses
{
    public class CardResponseModel
    {

        public string Id { get; set; }

        public string Iban { get; set; }

        public string CardNumber { get; set; }

        public DateTime ValidUntil { get; set; }

        public string TransactionNumber { get; set; }

        public double CurrentAmount { get; set; }

        public bool HasLimit { get; set; }

        public double? LimitAmount { get; set; }

        public List<SentTransactionsModel> SentTransactions { get; set; }
        public List<ReceivedTransactionsModel> ReceivedTransactions { get; set; }

    }
}

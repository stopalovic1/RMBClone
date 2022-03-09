using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class TransactionDBModel
    {
        public string Id { get; set; }
        public string CardId { get; set; }
        public string TransactionReason { get; set; }
        public double TransactionValue { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public TransactionDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

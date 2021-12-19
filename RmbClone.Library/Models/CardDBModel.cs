using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class CardDBModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Iban { get; set; }
        public string CardNumber { get; set; }
        public DateTime ValidUntil { get; set; }
        public string TransactionNumber { get; set; }
        public CardDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

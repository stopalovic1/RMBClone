using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmbClone.Library.Models
{
    public class CardDBModel
    {
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Iban { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public DateTime ValidUntil { get; set; }
        [Required]
        public string TransactionNumber { get; set; }
        [Required]
        public double CurrentAmount { get; set; }
        [Required]
        public bool HasLimit { get; set; }

        public double? LimitAmount { get; set; }

        public CardDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

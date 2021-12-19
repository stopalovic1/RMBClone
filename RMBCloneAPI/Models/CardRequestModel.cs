using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMBCloneAPI.Models
{
    public class CardRequestModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public DateTime ValidUntil { get; set; }
        [Required]
        public string TransactionNumber { get; set; }
    }
}

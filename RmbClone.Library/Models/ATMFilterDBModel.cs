using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class ATMFilterDBModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ATMFilterDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

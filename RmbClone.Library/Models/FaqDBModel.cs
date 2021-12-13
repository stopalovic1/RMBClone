using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class FaqDBModel
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public FaqDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

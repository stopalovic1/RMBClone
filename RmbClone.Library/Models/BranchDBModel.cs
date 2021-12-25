using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class BranchDBModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CityId { get; set; }
        public string Contact { get; set; }
        public string BranchTypeId { get; set; }
        public string BranchServiceTypeId { get; set; }
        public string ATMType { get; set; }
        public string ATMFilter { get; set; }
        public BranchDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

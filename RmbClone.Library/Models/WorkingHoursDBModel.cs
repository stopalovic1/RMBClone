using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class WorkingHoursDBModel
    {
        public string Id { get; set; }
        public string BranchId { get; set; }
        public string Day { get; set; }
        public string Hours { get; set; }
        public WorkingHoursDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

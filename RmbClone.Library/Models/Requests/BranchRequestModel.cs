using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmbClone.Library.Models.Requests
{
    public class BranchRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CityId { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public List<WorkingHoursRequestModel> WorkingHours { get; set; }
        [Required]
        public LocationRequestModel Location { get; set; }
        public string BranchTypeId { get; set; }
        public string BranchServiceTypeId { get; set; }
        [Required]
        public string ATMType { get; set; }
        [Required]
        public string ATMFilterId { get; set; }
    }
}

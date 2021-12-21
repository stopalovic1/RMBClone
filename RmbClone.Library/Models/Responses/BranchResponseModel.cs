using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models.Responses
{
    public class BranchResponseModel
    {
        public string Id { get; set; }
        public LocationDBModel Location { get; set; }
        public string Name { get; set; }
        public CityDBModel City { get; set; }
        public string Contact { get; set; }
        public List<WorkingHoursDBModel> WorkingHours { get; set; }
        public BranchTypeDBModel BranchType { get; set; }
        public BranchServiceTypeDBModel BranchServiceType { get; set; }
        public string ATMType { get; set; }
        public string ATMFilter { get; set; }
    }
}

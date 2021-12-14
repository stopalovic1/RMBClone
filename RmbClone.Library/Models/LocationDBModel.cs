using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class LocationDBModel
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public LocationDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

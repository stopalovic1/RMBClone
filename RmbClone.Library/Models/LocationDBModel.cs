using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmbClone.Library.Models
{
    public class LocationDBModel
    {
        public string Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double? Latitude { get; set; }
        [Required]
        public double? Longitude { get; set; }
        public LocationDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

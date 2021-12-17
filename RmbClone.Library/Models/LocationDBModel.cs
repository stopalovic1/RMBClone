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
        public decimal? Latitude { get; set; }
        [Required]
        public decimal? Longitude { get; set; }
        public LocationDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

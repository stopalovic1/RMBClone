using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RmbClone.Library.Models.Requests
{
    public class LocationRequestModel
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public decimal? Latitude { get; set; }
        [Required]
        public decimal? Longitude { get; set; }
    }
}

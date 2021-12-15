using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmbClone.Library.Models
{
    public class CityDBModel
    {
        
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public CityDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}

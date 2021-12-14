using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMBCloneAPI.Models
{
    public class CityRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmbClone.Library.Models.Requests
{
    public class RoleRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}

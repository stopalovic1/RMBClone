using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class RoleDBModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public RoleDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

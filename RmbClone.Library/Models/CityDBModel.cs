using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class CityDBModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CityDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}

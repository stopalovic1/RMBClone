using Microsoft.AspNetCore.Mvc.Rendering;
using RmbClone.Library.Models;
using System.Collections.Generic;
using System.Linq;


namespace RMBCloneAPI.Helpers
{
    public class HelperMethods : IHelperMethods
    {
        public List<SelectListItem> ATMType()
        {
            var atmTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "Unutrašnji",
                    Text = "Unutrašnji"
                },
                new SelectListItem
                {
                    Value = "Vanjski",
                    Text = "Vanjski"
                }
            };
            return atmTypes;
        }

        public List<SelectListItem> ATMFilter()
        {
            var atmFilters = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "CashIn",
                    Text = "CashIn"
                },
                new SelectListItem
                {
                    Value = "CashOut",
                    Text = "CashOut"
                },
                new SelectListItem
                {
                    Value = "Bankomatiq",
                    Text = "Bankomatiq"
                },
                new SelectListItem
                {
                    Value = "Mjenjačnica",
                    Text = "Mjenjačnica"
                }
            };
            return atmFilters;
        }
        public List<SelectListItem> MapCities(List<CityDBModel> collection)
        {
            var list = collection.Select(x =>
              new SelectListItem
              {
                  Value = x.Id,
                  Text = x.Name

              }).ToList();
            return list;
        }
    }
}

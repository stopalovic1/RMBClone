using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models;

namespace RMBCloneAPI.Pages.City
{
    public class DetailsModel : PageModel
    {
        private readonly ICityData _cityData;
        [BindProperty]
        public CityDBModel City { get; set; }
        public DetailsModel(ICityData cityData)
        {
            _cityData = cityData;
        }

        public async Task OnGet(string Id)
        {
            var city = await _cityData.FindCityByIdAsync(Id);
            City = city;
        }
    }
}

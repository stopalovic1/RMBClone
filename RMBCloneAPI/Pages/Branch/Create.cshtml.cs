using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models;
using RmbClone.Library.Models.Requests;
using RMBCloneAPI.Helpers;

namespace RMBCloneAPI.Pages.Branch
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ICityData _cityData;
        private readonly IHelperMethods _helpers;

        [BindProperty]
        public BranchRequestModel BranchRequest { get; set; }

        [BindProperty]
        public List<SelectListItem> Cities { get; set; }

        [BindProperty]
        public List<SelectListItem> ATMType { get; set; }

        [BindProperty]
        public List<SelectListItem> ATMFilter { get; set; } 
        public CreateModel(ICityData cityData,IHelperMethods helpers)
        {
            _cityData = cityData;
            _helpers = helpers;
        }
        public async Task OnGet()
        {
            await LoadData();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
               await LoadData();
            }
            else
            {
                TempData["BranchAdd"] = $"{BranchRequest.Name}";
                return RedirectToPage("/City/Create");
            }
            return Page();
        }

        private async Task LoadData()
        {
            var cities = await _cityData.GetAllCitiesAsync();
            Cities = _helpers.MapCities(cities);
            ATMType = _helpers.ATMType();
            ATMFilter = _helpers.ATMFilter();
        }
    }
}

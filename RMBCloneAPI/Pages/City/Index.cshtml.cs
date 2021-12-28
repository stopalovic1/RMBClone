using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models;
using RmbRazorPages.Library.ApiClient;

namespace RMBCloneAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICityData _cityData;
        private readonly IApiHelper _apiHelper;

        public IEnumerable<CityDBModel> Cities { get; set; }
        public IndexModel(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<IActionResult> OnGet()
        {
            var cities = await GetCities();
            if (cities.statusCode == 401)
            {
                return RedirectToPage("/Errors/401");
            }
            Cities = cities.cities;
            return Page();
        }


        private async Task<dynamic> GetCities()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/City"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<List<CityDBModel>>();

                    return new
                    {
                        cities = data,
                        statusCode = (int)response.StatusCode
                    };
                }
                return new
                {
                    cities = new List<CityDBModel>(),
                    statusCode = (int)response.StatusCode
                };
            }
        }



    }
}

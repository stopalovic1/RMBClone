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
        //private readonly ICityData _cityData;
        private readonly IApiHelper _apiHelper;

        public List<CityDBModel> Cities { get; set; }
        public IndexModel(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<IActionResult> OnGet()
        {
            var cities = await GetCities();
            if (cities == null)
            {
                Cities = new List<CityDBModel>();
                return RedirectToPage("/Errors/401", new { n = 401 });
            }
            Cities = cities;
            return Page();
        }


        private async Task<List<CityDBModel>> GetCities()
        {
            var token = HttpContext.Request.Cookies["SESSION_TOKEN"];

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://rmbcloneapi.azurewebsites.net/api/city"),
                Method = HttpMethod.Get
            };

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("Authorization", token);
            }

            using (HttpResponseMessage response = await _apiHelper.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<List<CityDBModel>>();

                    return data;
                }
                return null;
            }
        }
    }
}

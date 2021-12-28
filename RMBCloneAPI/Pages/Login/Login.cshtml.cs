using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RmbRazorPages.Library.ApiClient;

namespace RMBCloneAPI.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IApiHelper _apiHelper;

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public LoginModel(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        public IActionResult OnGet()
        {

            var token = HttpContext.Request.Cookies["SESSION_TOKEN"];
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Branch/Create");
            }
            return Page();
        }


        public async Task<IActionResult> OnPost()
        {

            //var query = $"email={UserName}&password={Password}";
            _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
            _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            //HttpContent data = new StringContent(query, Encoding.UTF8, "application/json");
            var data = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("grant_type","password"),
                new KeyValuePair<string, string>("email",UserName),
                new KeyValuePair<string, string>("password",Password)

            });
            try
            {
                using (HttpResponseMessage message = await _apiHelper.ApiClient.PostAsync("/token", data))
                {

                    if (message.IsSuccessStatusCode)
                    {
                        var result = await message.Content.ReadAsStringAsync();
                        //HttpContext.Session.SetString("JWToken", result);
                        HttpContext.Response.Cookies.Append("SESSION_TOKEN", "Bearer " + result,
                            new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(1),
                                HttpOnly = true,
                                Secure = false
                            });

                       _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {result}");

                        return RedirectToPage("/Branch/Create");
                    }
                    return Page();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
    }
}

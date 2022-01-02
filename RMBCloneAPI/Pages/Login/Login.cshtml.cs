using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RmbClone.Library.Models.Requests;
using RmbRazorPages.Library.ApiClient;
using Newtonsoft.Json;
using RmbClone.Library.Models.Responses;

namespace RMBCloneAPI.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IApiHelper _apiHelper;

        [BindProperty]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
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

            if (!ModelState.IsValid)
            {
                return Page();
            }
            //var query = $"email={UserName}&password={Password}";
            //HttpContent data = new StringContent(query, Encoding.UTF8, "application/json");
            var u = new AuthRequestModel
            {
                Email = Email,
                Password = Password
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email",Email),
                new KeyValuePair<string, string>("password",Password)
            });
            try
            {
                using (HttpResponseMessage message = await _apiHelper.ApiClient.PostAsync("/api/token", stringContent))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        var result = await message.Content.ReadAsAsync<AuthResponseModel>();
                        //var result = await message.Content.ReadAsAsync<string>();
                        //HttpContext.Session.SetString("JWToken", result);
                        HttpContext.Response.Cookies.Append("SESSION_TOKEN", "Bearer " + result.AccessToken,
                            new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(1),
                                HttpOnly = true,
                                Secure = false
                            });

                        return RedirectToPage("/Branch/Create");
                    }
                    ModelState.AddModelError(string.Empty, "Incorrect email or password.");
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

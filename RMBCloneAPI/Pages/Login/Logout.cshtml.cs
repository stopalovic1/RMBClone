using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RmbRazorPages.Library.ApiClient;

namespace RMBCloneAPI.Pages.Login
{
    public class LogoutModel : PageModel
    {


        public IActionResult OnGet()
        {
            if (HttpContext.Request.Cookies["SESSION_TOKEN"] != null)
            {
                return RedirectToPage("/Branch/Create");
            }
            return RedirectToPage("/Login/Login");
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Request.Cookies["SESSION_TOKEN"] != null)
            {
                var token = HttpContext.Request.Cookies["SESSION_TOKEN"];
                var c = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1),
                    HttpOnly = true,
                    Secure = false
                };
                HttpContext.Response.Cookies.Append("SESSION_TOKEN", token, c);
            }
            return RedirectToPage("/Login/Login");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RMBCloneAPI.Pages.Errors
{
    public class _401Model : PageModel
    {
        public IActionResult OnGet(int n)
        {
            if (n == 401)
            {
                return Page();
            }
            return RedirectToPage("/Login/Login");
        }
    }
}

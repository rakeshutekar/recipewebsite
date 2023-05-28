using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// SignInModel represents the page for signing into the website
    /// </summary>
    public class SignInModel : PageModel
    {
        /// <summary>
        /// OnGet method prepares the page on pafe load
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// OnPost action after user selects the sign-in button
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// SignUpModel represents the page for creating an account
    /// </summary>
	public class SignUpModel : PageModel
    {
        /// <summary>
        /// OnGet method prepares the page on page load
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// OnPost action after user selects the sign-up button
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            return Page();
        }
    }
}

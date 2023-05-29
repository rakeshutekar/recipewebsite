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
        // Private logger class field
        private readonly ILogger<SignInModel> _logger;

        /// <summary>
        /// Constructor injects logger class into the page
        /// </summary>
        /// <param name="logger">Logger service injected via DI</param>
        public SignInModel(ILogger<SignInModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// OnGet method prepares the page on page load
        /// </summary>
        public IActionResult OnGet()
        {
            return Page();
        }

        /// <summary>
        /// OnPost action after user selects the sign-in button
        /// </summary>
        /// <returns>Returns user to the same page as before</returns>
        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
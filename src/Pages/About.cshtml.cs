using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents a model for the About page that provides information about the application.
    /// </summary>
    public class AboutModel : PageModel
    {
        private readonly ILogger<AboutModel> _logger;

        /// <summary>
        /// Initializes a new instance of the About class with the specified logger.
        /// </summary>
        /// <param name="logger">The logger to use for logging information about the application.</param>
        public AboutModel(ILogger<AboutModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles HTTP GET requests for the About page.
        /// </summary>
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents a model for the About page that provides information about the application.
    /// </summary>
    public class About : PageModel
    {
        private readonly ILogger<About> _logger;

        /// <summary>
        /// Initializes a new instance of the About class with the specified logger.
        /// </summary>
        /// <param name="logger">The logger to use for logging information about the application.</param>
        public About(ILogger<About> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles HTTP GET requests for the About page.
        /// </summary>
        public void OnGet()
        {
        }
    }
}
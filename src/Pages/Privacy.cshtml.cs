using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents a model for the Privacy page that provides privacy policy information.
    /// </summary>
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// Initializes a new instance of the PrivacyModel class with the specified logger.
        /// </summary>
        /// <param name="logger">The logger to use for logging privacy-related information.</param>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles HTTP GET requests for the Privacy page.
        /// </summary>
        public void OnGet()
        {
        }
    }
}

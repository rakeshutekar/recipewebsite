using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Represents a model for handling and displaying errors that occur during the processing of a Razor Page.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// Gets or sets the request ID associated with the current request.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request ID should be shown.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// The logger used for logging errors in the ErrorModel class.
        /// </summary>
        private readonly ILogger<ErrorModel> _logger;


        /// <summary>
        /// Initializes a new instance of the ErrorModel class with the specified logger.
        /// </summary>
        /// <param name="logger">The logger to use for logging errors.</param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Sets the value of the RequestId variable to the ID of the current activity, or to the trace identifier
        /// of the HTTP context if the current activity is null.
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
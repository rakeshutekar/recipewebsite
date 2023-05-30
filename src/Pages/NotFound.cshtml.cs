using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Class to help provide users with information about the page they are
    /// trying to access is not possible
    /// </summary>
    public class NotFoundModel : PageModel
    {
        public const string NOT_FOUND_RESPONSE = "The recipe was not found";
        public const string DEFAULT_RESPONSE = "The page you are looking for could not be found";

        // Enum type used to represent the error type shown
        [BindProperty(SupportsGet = true)]
        public NotFoundTypes? type { get; set; }

        // Response to the
        public string response { get; set; }

        /// <summary>
        /// On get method sets the initial values of the page on load
        /// </summary>
        public void OnGet()
        {
            // Determine the type of resposne based on the value passed in
            switch(type)
            {
                case NotFoundTypes.Recipe:
                    {
                        response = NOT_FOUND_RESPONSE;
                        break;
                    }
                default:
                    {
                        response = DEFAULT_RESPONSE;
                        break;
                    }
            }
        }
    }

    /// <summary>
    /// Enum to define the types of values that are not found
    /// </summary>
    public enum NotFoundTypes
    {
        None = 0,
        Recipe = 1
    }
}

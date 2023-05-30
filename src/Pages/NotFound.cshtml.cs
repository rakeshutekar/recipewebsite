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
        // Response when Not Found Type = recipe
        public const string RECIPE_NOT_FOUND_RESPONSE = "The recipe was not found";
        // Response when Not Found Type = Default
        public const string DEFAULT_NOT_FOUND_RESPONSE = "The page you are looking for could not be found";

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
                        response = RECIPE_NOT_FOUND_RESPONSE;
                        break;
                    }
                default:
                    {
                        response = RECIPE_DEFAULT_RESPONSE;
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

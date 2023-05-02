using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages
{
    // CreateModel class represents the page model for the recipe creation page
    public class CreateModel : PageModel
    {
        // Recipe property represents the model for the recipe data that will be submitted
        [BindProperty]
        public Recipe Recipe { get; set; }

        // OnGet method handles GET requests for the recipe creation page
        public void OnGet()
        {
        }

        // OnPostAsync method handles POST requests for the recipe creation page
       public async Task<IActionResult> OnPostAsync()
        {
            await Task.Yield();
            // Save the recipe to the database or perform other actions here
            // For demonstration purposes, we'll just return to the home page
            return RedirectToPage("/Index");
        }

    }

    // Recipe class represents the model for the recipe data that will be submitted
    public class Recipe
    {
        public string ImgLink { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
    }
}

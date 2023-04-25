using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// RecipeModel class is used to represent recipe JSON data in object form
    /// </summary>
    public class RecipeModel
    {
        /*PRIVATE FIELDS*/
        // Default description when the description field is empty
        private string description = "Default Description";

        /*PUBLIC VARIABLES*/
        // ID of the recipe from the JSON file
        public int RecipeID{get;set;}
        // ID of the author from the JSON file
        public int AuthorID{get;set;}
        // First name of the writer
        public string FirstName{get;set;}
        // Last name of the writer
        public string LastName{get;set;}
        // Title of the recipe
        public string Title{get;set;}
        // Short description of the recipe
        public string Description
        {
            get => description;
            set
            {
                if (value.Length <= 1) return;
                description = value;
            }
        }
        // Array of instructions for the recipe
        public string[] Instructions{get;set;}
        // Array of ingredients for the recipe
        public string[] Ingredients{get;set;}
        // Date of publishing the recipe on the webiste
        public string PublishDate{get;set;}
        // Date of last edit of the recipe 
        public string EditDate{get;set;}
        // Tags associated with the recipe, used for filtering
        public string[] Tags{get;set;}
        // Add a new property for the image caption
        public string ImageCaption { get; set; }
        // Image URL of imgae associated with the recipe
        [JsonPropertyName("Img")]
        public string Image{get;set;}

        // Overridden ToString method to serialize the object's data
        public override string ToString() => JsonSerializer.Serialize<RecipeModel>(this);
 
    }
}
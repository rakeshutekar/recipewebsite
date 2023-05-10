using System;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

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
        // Deleted flag to indicate whether to show or not
        public bool Deleted {get;set;}
        // ID of the recipe from the JSON file
        public int RecipeID{get;set;}
        // ID of the author from the JSON file
        public int AuthorID{get;set;}
        // First name of the writer
        public string FirstName{get;set;}
        // Last name of the writer
        public string LastName{get;set;}
        // Replace the following line with the modified Title property
        // public string Title{get;set;}

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Recipe title must be between 3 and 100 characters.")]
        [RegularExpression(@"^[\w\s\-\,\(\)]+$", ErrorMessage = "Recipe title can only contain letters, numbers, spaces, dashes, commas, and parentheses.")]
        public string Title { get; set; }

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
        // ... Existing RecipeModel.cs content ...

        public class InstructionsAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var list = value as IList;
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (string.IsNullOrWhiteSpace(item as string) || (item as string).Length > 500)
                        {
                            return new ValidationResult("Each instruction must be between 1 and 500 characters.");
                        }
                    }
                }
                return ValidationResult.Success;
            }
        }
        [Instructions]
        public string[] Instructions { get; set; }
        // Define a custom validation attribute named IngredientsAttribute
        public class IngredientsAttribute : ValidationAttribute
        {
            // Override the IsValid method for custom validation logic
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                // Try to cast the value to an IList
                var list = value as IList;
                
                // If the cast was successful
                if (list != null)
                {
                    // Iterate through each item in the list
                    foreach (var item in list)
                    {
                        // If the item is null, empty, whitespace, or longer than 200 characters
                        if (string.IsNullOrWhiteSpace(item as string) || (item as string).Length > 200)
                        {
                            // Return a validation result indicating the failure
                            return new ValidationResult("Each ingredient must be between 1 and 200 characters.");
                        }
                    }
                }
                
                // If the list was null or all items passed the validation, return success
                return ValidationResult.Success;
            }
        }

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
         // Required: This attribute specifies that the Image property must have a value; it cannot be null or empty.
            [Required]

            // Url: This attribute specifies that the Image property must be a valid URL. 
            // It uses a regular expression (regex) to validate the input as a URL.
            [Url]

            // Display: This attribute allows us to specify a display name for the Image property. 
            // In this case, "Image URL" will be used as the display name instead of the property name "Image".
            [Display(Name = "Image URL")]
            public string Image { get; set; }

        /// <summary>
        /// Overridden ToString method to serialize the object's data
        /// </summary>
        /// <returns>String/JSON representation of object</returns>
        public override string ToString() => JsonSerializer.Serialize<RecipeModel>(this);

        public static RecipeModel TEST_VAL
        {
            get
            {
                var newRecipe = new RecipeModel();
                newRecipe.Deleted = false;
                newRecipe.AuthorID = 0;
                newRecipe.FirstName = "TEST";
                newRecipe.LastName = "TEST";
                newRecipe.Title = "TEST";
                newRecipe.Instructions = new string[] { "TEST" };
                newRecipe.Ingredients = new string[] { "TEST" };
                newRecipe.Tags = new string[] { "TEST" };
                newRecipe.PublishDate = "TEST";
                newRecipe.EditDate = "TEST";
                newRecipe.ImageCaption = "TEST";
                newRecipe.Image = "TEST";
                return newRecipe;
            }
        }

    }
}
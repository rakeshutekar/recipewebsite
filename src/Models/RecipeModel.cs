using System;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Models
{
    public enum Reaction
    {
        Sad = 1, 
        Content = 2, 
        Happy = 3
    }

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

        /// <summary>
        /// Defines a custom validation attribute named InstructionssAttribute to help with validating user input
        /// </summary>
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

        /// <summary>
        /// Defines a custom validation attribute named IngredientsAttribute to help with validating user input
        /// </summary>
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

        // List of CommentModels that stores each comment
        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();

        #region Reactions
        // List of all reactions to this recipe
        public List<Reaction> Reactions { get;set; }

        public Dictionary<Reaction, int> ReactionCharacters = new Dictionary<Reaction, int>()
        {
            {Reaction.Sad,  128549},
            {Reaction.Content,128528 },
            {Reaction.Happy, 128525 }
        };

        public List<(Reaction, int)> GetReactions()
        {
            // Return default reaction list with 0 reaction
            if (Reactions == null) return new List<(Reaction, int)>()
            {
                (Reaction.Sad, 0),
                (Reaction.Content, 0),
                (Reaction.Happy, 0),
            };

            // Ensure that reaction count dictionary contains all Reaction types
            Dictionary<Reaction, int> reactionCount = new Dictionary<Reaction, int>()
            {
                {Reaction.Sad,0 },
                {Reaction.Content, 0},
                {Reaction.Happy, 0 }
            };

            // This will sum up all reaction but does not check if reaction type
            // is currently in the dictionary - if new reactions are add
            // ensure that they are added above in dictionary initializer
            foreach(var reaction in Reactions) reactionCount[reaction]++;


            List<(Reaction, int)> countList = new List<(Reaction, int)>();
            foreach (KeyValuePair<Reaction, int> pair in reactionCount)
            {
                countList.Add((pair.Key, pair.Value));
            }

            return countList;
        }
        #endregion

        /// <summary>
        /// Overridden ToString method to serialize the object's data
        /// </summary>
        /// <returns>String/JSON representation of object</returns>
        public override string ToString() => JsonSerializer.Serialize<RecipeModel>(this);
    }
}
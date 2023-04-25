using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class RecipeModel
    {
        private string description = "Default Description";

        public int RecipeID{get;set;}
        public int AuthorID{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string Title{get;set;}
        public string Description
        {
            get => description;
            set
            {
                if (value.Length <= 1) return;
                description = value;
            }
        }
        public string[] Instructions{get;set;}
        public string[] Ingredients{get;set;}
        public string PublishDate{get;set;}
        public string EditDate{get;set;}
        public string[] Tags{get;set;}
        [JsonPropertyName("Img")]
        public string Image{get;set;}

        public override string ToString() => JsonSerializer.Serialize<RecipeModel>(this);
 
    }
}
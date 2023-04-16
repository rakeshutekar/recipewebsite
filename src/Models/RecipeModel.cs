using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class RecipeModel
    {
        public string RecipeID{get;set;}
        public string AuthorID{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        public string Title{get;set;}
        public string Description{get;set;}
        public string PublishDate{get;set;}
        public string EditDate{get;set;}
        public string[] Tags{get;set;}
        [JsonPropertyName("Img")]
        public string Img{get;set;}

        public override string ToString() => JsonSerializer.Serialize<RecipeModel>(this);
 
    }
}
using System;
namespace ContosoCrafts.WebSite.Models
{
	/// <summary>
	/// Comments submitted by users are captured in a model associated with a
	/// single recipe
	/// </summary>
	public class CommentModel
	{
		// Unique id of the comment
		public string Id { get; set; } = System.Guid.NewGuid().ToString();

		// First Name of the user submitting the comment
		public string FirstName { get; set; }

		// Last name of the user submitting the comment
		public string LastName { get; set; }

		// Comment submitted by the user
		public string Comment { get; set; }
	}
}
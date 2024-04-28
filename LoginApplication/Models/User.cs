using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LoginApplication.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "This field is required")]
		public string Username { get; set; } = string.Empty;

		//[Required(ErrorMessage = "This field is required")]
		public string Password { get; set; }

		[Display(Name = "Admin")]
		public bool IsAdmin { get; set; }
		public bool IsLoggedIn { get; set; }
	}
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace agSalon.Services.ViewModels
{
	public class RegisterVM
	{
		[Required]
		[StringLength(20)]
		public string Name { get; set; }

		[Required]
		[StringLength(45)]
		public string Surname { get; set; }

		[Required]
		[StringLength(13), DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime DateBirth { get; set; } = DateTime.Today;



		[Display(Name = "Email address")]
		[Required(ErrorMessage = "Email address is required")]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm password is required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords don't match")]
		public string ConfirmPassword { get; set; }
	}
}

using agSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Services.ViewModels
{
    public class WorkerVM
    {
        public string? Id { get; set; }

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

		[Required]
        [StringLength(45)]
        public string Address { get; set; }


		[StringLength(6)]
		public string Gender { get; set; }

		[Required(ErrorMessage = "Group is required")]
		public List<int> GroupsIds { get; set; }


		[Display(Name = "Email address")]
		[Required(ErrorMessage = "Email address is required")]
		public string EmailAddress { get; set; }

		
		public List<Schedule> Schedules { get; set; } = new List<Schedule>();
	}
}

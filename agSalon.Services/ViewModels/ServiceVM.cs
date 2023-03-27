using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Services.ViewModels
{
	public class ServiceVM
	{
		public int Id { get; set; }

		[Required]
		[StringLength(45)]
		public string Name { get; set; }

		[Required]
		public double Price { get; set; }

		public int GroupId { get; set; }
	}
}

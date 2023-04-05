using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Domain.Entities
{
	public class Schedule
	{
		[Column("worker_id")]
		public string WorkerId { get; set; }
		public Worker Worker { get; set; }


		[StringLength(10)]
		[Column("day")]
		public string Day { get; set; }


		[Column("start", TypeName = "time")]
		public TimeSpan Start { get; set; }

		[Column("end", TypeName = "time")]
		public TimeSpan End { get; set; }
	}
}

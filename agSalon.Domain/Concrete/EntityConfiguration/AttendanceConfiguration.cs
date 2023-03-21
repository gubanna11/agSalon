using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using agSalon.Domain.Entities;
using System.Reflection.Emit;

namespace agSalon.Domain.Concrete.EntityConfiguration
{
	public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
	{
		public void Configure(EntityTypeBuilder<Attendance> builder)
		{
			builder.HasIndex(att => new { att.ClientId, att.Date, att.ServiceId }).IsUnique();
			builder.HasIndex(att => new { att.WorkerId, att.Date, att.ServiceId }).IsUnique();
			builder.Property(a => a.Time).HasColumnType("time");
		}
	}
}

using agSalon.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace agSalon.Domain.Concrete.EntityConfiguration
{
	public class GroupOfServicesConfiguration : IEntityTypeConfiguration<GroupOfServices>
	{
		public void Configure(EntityTypeBuilder<GroupOfServices> builder)
		{
			builder.HasIndex(g => g.Name).IsUnique();
		}
	}
}

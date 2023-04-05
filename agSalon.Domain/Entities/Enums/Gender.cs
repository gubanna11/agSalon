using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Domain.Entities.Enums
{
    public enum Gender
    {
		[Description("Female")]
		Female,

		[Description("Male")]
		Male,
	}
}

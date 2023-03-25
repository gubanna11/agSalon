using agSalon.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Domain.Concrete
{
	public class AppDbInitializer
	{

		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

				if (!context.Groups.Any())
				{
					context.Groups.AddRange(new List<GroupOfServices>()
					{
						new GroupOfServices()
						{
							Name = "Cosmetology"
						},
						new GroupOfServices()
						{
							Name = "Massages"
						},
						new GroupOfServices()
						{
							Name = "Nail service"
						},
						new GroupOfServices()
						{
							Name = "Hairdresser's"
						},
						new GroupOfServices()
						{
							Name = "Makeup"
						},
						new GroupOfServices()
						{
							Name = "Depilation"
						}
					});

					context.SaveChanges();
				}

				if (!context.Services.Any())
				{
					context.Services.AddRange(new List<Service>()
					{
						new Service()
						{
							Name = "Eyebrow correction",
							Price = 80
						},
						new Service()
						{
							Name = "Ultrasonic cleaning",
							Price = 600
						},
						new Service()
						{
							Name = "Mechanical cleaning",
							Price = 500
						},
						new Service()
						{
							Name = "Combined facial cleansing",
							Price = 550
						},
						new Service()
						{
							Name = "Facial massage",
							Price = 200
						},
						new Service()
						{
							Name = "Mechanical peeling",
							Price = 600
						},
						new Service()
						{
							Name = "Chemical peeling",
							Price = 550
						},
						new Service()
						{
							Name = "Therapeutic massage",
							Price = 400
						},
						new Service()
						{
							Name = "Classic massage",
							Price = 300
						},
						new Service()
						{
							Name = "Lymphatic drainage massage",
							Price = 500
						},
						new Service()
						{
							Name = "Manicure",
							Price = 200
						},
						new Service()
						{
							Name = "Covering with gel varnish",
							Price = 180
						},
						new Service()
						{
							Name = "Shellac",
							Price = 200
						},
						new Service()
						{
							Name = "Pedicure",
							Price = 300
						},
						new Service()
						{
							Name = "Children's haircut",
							Price = 80
						},
						new Service()
						{
							Name = "Women's haircut",
							Price = 120
						},
						new Service()
						{
							Name = "Men's haircut",
							Price = 100
						},
						new Service()
						{
							Name = "Dyeing",
							Price = 350
						},
						new Service()
						{
							Name = "Drawing",
							Price = 600
						},
						new Service()
						{
							Name = "Day makeup",
							Price = 450
						},
						new Service()
						{
							Name = "Evening makeup",
							Price = 500
						},
						new Service()
						{
							Name = "Makeup express",
							Price = 300
						},
						new Service()
						{
							Name = "Waxing - legs",
							Price = 200
						},
						new Service()
						{
							Name = "Waxing - hands",
							Price = 170
						},
						new Service()
						{
							Name = "Waxing - bikini",
							Price = 250
						},
					});
					context.SaveChanges();
				}

				if (!context.Services_Groups.Any())
				{
					context.Services_Groups.AddRange(new List<Service_Group>()
					{
						new Service_Group()
						{
							ServiceId = 1,
							GroupId = 1
						},
						new Service_Group()
						{
							ServiceId = 2,
							GroupId = 1
						},
						new Service_Group()
						{
							ServiceId = 3,
							GroupId = 1
						},
						new Service_Group()
						{
							ServiceId = 4,
							GroupId = 1
						},
						new Service_Group()
						{
							ServiceId = 5,
							GroupId = 1
						},
						new Service_Group()
						{
							ServiceId = 6,
							GroupId = 1
						},
						new Service_Group()
						{
							ServiceId = 7,
							GroupId = 1
						},
						new Service_Group()
						{
							ServiceId = 8,
							GroupId = 2
						},
						new Service_Group()
						{
							ServiceId = 9,
							GroupId = 2
						},
						new Service_Group()
						{
							ServiceId = 10,
							GroupId = 2
						},
						new Service_Group()
						{
							ServiceId = 11,
							GroupId = 3
						},
						new Service_Group()
						{
							ServiceId = 12,
							GroupId = 3
						},
						new Service_Group()
						{
							ServiceId = 13,
							GroupId = 3
						},
						new Service_Group()
						{
							ServiceId = 14,
							GroupId = 3
						},
						new Service_Group()
						{
							ServiceId = 15,
							GroupId = 4
						},
						new Service_Group()
						{
							ServiceId = 16,
							GroupId = 4
						},
						new Service_Group()
						{
							ServiceId = 17,
							GroupId = 4
						},
						new Service_Group()
						{
							ServiceId = 18,
							GroupId = 4
						},
						new Service_Group()
						{
							ServiceId = 19,
							GroupId = 4
						},
						new Service_Group()
						{
							ServiceId = 20,
							GroupId = 5
						},
						new Service_Group()
						{
							ServiceId = 21,
							GroupId = 5
						},
						new Service_Group()
						{
							ServiceId = 22,
							GroupId = 5
						},
						new Service_Group()
						{
							ServiceId = 23,
							GroupId = 6
						},
						new Service_Group()
						{
							ServiceId = 24,
							GroupId = 6
						},
						new Service_Group()
						{
							ServiceId = 25,
							GroupId = 6
						},
					});

					context.SaveChanges();
				}
			}
		}

	}
}

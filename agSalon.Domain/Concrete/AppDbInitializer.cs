using agSalon.Domain.Entities;
using agSalon.Domain.Entities.Enums;
using agSalon.Domain.Entities.Static;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
					context.Services_Groups.AddRange(new List<ServiceGroup>()
					{
						new ServiceGroup()
						{
							ServiceId = 1,
							GroupId = 1
						},
						new ServiceGroup()
						{
							ServiceId = 2,
							GroupId = 1
						},
						new ServiceGroup()
						{
							ServiceId = 3,
							GroupId = 1
						},
						new ServiceGroup()
						{
							ServiceId = 4,
							GroupId = 1
						},
						new ServiceGroup()
						{
							ServiceId = 5,
							GroupId = 1
						},
						new ServiceGroup()
						{
							ServiceId = 6,
							GroupId = 1
						},
						new ServiceGroup()
						{
							ServiceId = 7,
							GroupId = 1
						},
						new ServiceGroup()
						{
							ServiceId = 8,
							GroupId = 2
						},
						new ServiceGroup()
						{
							ServiceId = 9,
							GroupId = 2
						},
						new ServiceGroup()
						{
							ServiceId = 10,
							GroupId = 2
						},
						new ServiceGroup()
						{
							ServiceId = 11,
							GroupId = 3
						},
						new ServiceGroup()
						{
							ServiceId = 12,
							GroupId = 3
						},
						new ServiceGroup()
						{
							ServiceId = 13,
							GroupId = 3
						},
						new ServiceGroup()
						{
							ServiceId = 14,
							GroupId = 3
						},
						new ServiceGroup()
						{
							ServiceId = 15,
							GroupId = 4
						},
						new ServiceGroup()
						{
							ServiceId = 16,
							GroupId = 4
						},
						new ServiceGroup()
						{
							ServiceId = 17,
							GroupId = 4
						},
						new ServiceGroup()
						{
							ServiceId = 18,
							GroupId = 4
						},
						new ServiceGroup()
						{
							ServiceId = 19,
							GroupId = 4
						},
						new ServiceGroup()
						{
							ServiceId = 20,
							GroupId = 5
						},
						new ServiceGroup()
						{
							ServiceId = 21,
							GroupId = 5
						},
						new ServiceGroup()
						{
							ServiceId = 22,
							GroupId = 5
						},
						new ServiceGroup()
						{
							ServiceId = 23,
							GroupId = 6
						},
						new ServiceGroup()
						{
							ServiceId = 24,
							GroupId = 6
						},
						new ServiceGroup()
						{
							ServiceId = 25,
							GroupId = 6
						},
					});

					context.SaveChanges();
				}
			}
		}

		public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

				if (!await roleManager.RoleExistsAsync(UserRoles.Worker))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Worker));

				if (!await roleManager.RoleExistsAsync(UserRoles.Client))
					await roleManager.CreateAsync(new IdentityRole(UserRoles.Client));


				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Client>>();

				string adminEmail = "admin@gmail.com";
				var adminUser = await userManager.FindByEmailAsync(adminEmail);

				if (adminUser == null)
				{
					var newAdminUser = new Client()
					{
						Name = "Admin",
						Surname = "Admin",
						PhoneNumber = "+380662738199",
						DateBirth = new DateTime(1990, 2, 3),
						Email = adminEmail,
						EmailConfirmed = true,
						UserName = adminEmail
					};
					await userManager.CreateAsync(newAdminUser, "Coding@1234?");
					await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
				}


				string userEmail = "client@gmail.com";
				var clientUser = await userManager.FindByEmailAsync(userEmail);

				if (clientUser == null)
				{
					var newClientUser = new Client()
					{
						Name = "Client",
						Surname = "Client",
						PhoneNumber = "+380993212483",
						DateBirth = new DateTime(1980, 3, 20),
						Email = userEmail,
						EmailConfirmed = true,
						UserName = userEmail
					};
					await userManager.CreateAsync(newClientUser, "Coding@1234?");
					await userManager.AddToRoleAsync(newClientUser, UserRoles.Client);
				}



				var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

				if (!context.Workers.Any())
				{
					string workerEmail = "worker@gmail.com";
					var workerUser = await userManager.FindByEmailAsync(workerEmail);

					if (workerUser == null)
					{
						var newWorkerUser = new Client()
						{
							Name = "Worker",
							Surname = "Worker",
							PhoneNumber = "+380989441213",
							DateBirth = new DateTime(1980, 3, 20),
							Email = workerEmail,
							EmailConfirmed = true,
							UserName = workerEmail
						};
						await userManager.CreateAsync(newWorkerUser, "Coding@1234?");
						await userManager.AddToRoleAsync(newWorkerUser, UserRoles.Worker);


						var newWorker = new Worker()
						{
							Id = newWorkerUser.Id,
							Address = "Workers's Address",
							Gender = Gender.Female
						};

						context.Workers.Add(newWorker);

						context.Workers_Groups.AddRange(
							new Worker_Group()
							{
								WorkerId = newWorker.Id,
								GroupId = 1
							},
							new Worker_Group()
							{
								WorkerId = newWorker.Id,
								GroupId = 6
							}
						);

						context.SaveChanges();
					}


					string worker2Email = "worker2@gmail.com";
					var worker2User = await userManager.FindByEmailAsync(worker2Email);

					if (worker2User == null)
					{
						var newWorkerUser = new Client()
						{
							Name = "Worker2",
							Surname = "Worker",
							PhoneNumber = "+380989441213",
							DateBirth = new DateTime(1980, 3, 20),
							Email = worker2Email,
							EmailConfirmed = true,
							UserName = worker2Email
						};
						await userManager.CreateAsync(newWorkerUser, "Coding@1234?");
						await userManager.AddToRoleAsync(newWorkerUser, UserRoles.Worker);


						var newWorker = new Worker()
						{
							Id = newWorkerUser.Id,
							Address = "Workers's 2 Address",
							Gender = Gender.Female
						};

						context.Workers.Add(newWorker);

						context.Workers_Groups.AddRange(
							new Worker_Group()
							{
								WorkerId = newWorker.Id,
								GroupId = 2
							},
							new Worker_Group()
							{
								WorkerId = newWorker.Id,
								GroupId = 6
							}
						);

						context.SaveChanges();

					}
				}

			}
		}

	}
}

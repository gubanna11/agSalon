using agSalon.Domain.Abstract.Repositories;
using agSalon.Domain.Concrete;
using agSalon.Domain.Entities;
using agSalon.Services.Services.Interfaces;
using agSalon.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Services.Services.Implementations
{
	public class ServicesService : EntityBaseRepository<Service>, IServicesService
	{
		private readonly AppDbContext _context;
		public ServicesService(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public List<Service> GetServicesByGroupId(int groupId)
			=> _context.Services.Include(s => s.Service_Group).Where(s => s.Service_Group.GroupId == groupId).ToList();


		public async Task AddNewServiceAsync(ServiceVM newService)
		{
			Service service = new Service
			{
				Name = newService.Name,
				Price = newService.Price
			};

			await _context.Services.AddAsync(service);
			await _context.SaveChangesAsync();

			Service_Group serviceGroup = new Service_Group
			{
				ServiceId = service.Id,
				GroupId = newService.GroupId
			};

			await _context.Services_Groups.AddAsync(serviceGroup);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateServiceAsync(ServiceVM serviceVM)
		{
			var service = await GetByIdAsync(serviceVM.Id);

			if (service != null)
			{
				service.Name = serviceVM.Name;
				service.Price = serviceVM.Price;
			}

			var service_group = _context.Services_Groups.Where(sg => sg.ServiceId == service.Id).FirstOrDefault();
			_context.Services_Groups.Remove(service_group);

			Service_Group newService_Group = new Service_Group()
			{
				ServiceId = service.Id,
				GroupId = serviceVM.GroupId
			};

			await _context.Services_Groups.AddAsync(newService_Group);
			await _context.SaveChangesAsync();
		}

		public async Task<Service> GetServiceByIdWithGroupAsync(int id) =>
			await _context.Services.Where(s => s.Id == id)
			.Include(s => s.Service_Group).ThenInclude(sg => sg.Group).FirstOrDefaultAsync();
	}
}

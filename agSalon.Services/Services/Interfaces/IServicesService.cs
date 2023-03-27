using agSalon.Domain.Abstract.Repositories;
using agSalon.Domain.Entities;
using agSalon.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Services.Services.Interfaces
{
	public interface IServicesService : IEntityBaseRepository<Service>
	{
		Task<Service> GetServiceByIdWithGroupAsync(int id);
		List<Service> GetServicesByGroupId(int groupId);
		Task AddNewServiceAsync(ServiceVM newService);
		Task UpdateServiceAsync(ServiceVM serviceVM);
	}
}

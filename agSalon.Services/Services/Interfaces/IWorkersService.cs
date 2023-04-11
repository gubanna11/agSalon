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
	public interface IWorkersService 
	{
		Task<WorkerVM> GetWorkerVMByIdAsync(string workerId);
		Task<Worker> GetWorkerByIdAsync(string workerId);

		Task<IEnumerable<Worker>> GetWorkersAsync();



		Task UpdateWorkerAsync(WorkerVM workerVM);

		Task DeleteAsync(string workerId);

	}
}

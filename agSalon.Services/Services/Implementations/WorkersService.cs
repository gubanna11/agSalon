using agSalon.Domain.Abstract.Repositories;
using agSalon.Domain.Concrete;
using agSalon.Domain.Entities;
using agSalon.Services.Services.Interfaces;
using agSalon.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace agSalon.Services.Services.Implementations
{
	public class WorkersService : IWorkersService
	{
		private readonly AppDbContext _context;

		public WorkersService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Worker>> GetWorkersAsync()
		{
			return await _context.Workers.Include(w => w.Client).Include(w => w.Schedules)
				.Include(w => w.Workers_Groups)
					.ThenInclude(wg => wg.Group).ToListAsync();
		}



		public async Task<WorkerVM> GetWorkerVMByIdAsync(string workerId)
		{
			WorkerVM workerVM = new WorkerVM();

			var worker = await _context.Workers.Where(w => w.Id == workerId).Include(w => w.Client)
				.Include(w => w.Schedules)
				.Include(w => w.Workers_Groups).ThenInclude(wg => wg.Group).FirstOrDefaultAsync();

			workerVM.Id = workerId;
			workerVM.Phone = worker.Client.PhoneNumber;
			workerVM.Address = worker.Address;
			workerVM.EmailAddress = worker.Client.Email;
			workerVM.Name = worker.Client.Name;
			workerVM.Surname = worker.Client.Surname;
			workerVM.GroupsIds = worker.Workers_Groups.Select(wg => wg.GroupId).ToList();
			workerVM.DateBirth = worker.Client.DateBirth;
			workerVM.Gender = worker.Gender;
			workerVM.Schedules = worker.Schedules;

			return workerVM;
		}


		public async Task<Worker> GetWorkerByIdAsync(string workerId)
		{
			return await _context.Workers.Where(w => w.Id == workerId).Include(w => w.Schedules).FirstOrDefaultAsync();
		}



		private async Task UpdateWorkerInfo(WorkerVM workerVM)
		{
			//var worker = await GetWorkerByIdAsync(workerVM.Id);
			var worker = await _context.Workers.Where(w => w.Id == workerVM.Id).Include(w => w.Client).FirstOrDefaultAsync();
			//_context.Entry(worker).State = EntityState.Unchanged;
			if (worker is not null)
			{
				worker.Address = workerVM.Address;
				worker.Gender = workerVM.Gender;


				worker.Client.Name = workerVM.Name;
				worker.Client.Surname = workerVM.Surname;
				worker.Client.Email = workerVM.EmailAddress;

				await _context.SaveChangesAsync();
				_context.Entry(worker.Client).State = EntityState.Detached;
				_context.Entry(worker).State = EntityState.Detached;
			}

		}

		private async Task UpdateWorkersGroups(WorkerVM workerVM)
		{
			var worker_groups = _context.Workers_Groups.AsNoTracking().Where(wg => wg.WorkerId == workerVM.Id).ToList();

			_context.Workers_Groups.RemoveRange(worker_groups);
			await _context.SaveChangesAsync();

			worker_groups.ForEach(wg => { _context.Entry(wg).State = EntityState.Detached; });

			var list = new List<Worker_Group>();
			foreach (var groupId in workerVM.GroupsIds)
			{
				Worker_Group wg = new Worker_Group
				{
					WorkerId = workerVM.Id,
					GroupId = groupId,
				};

				list.Add(wg);
			}

			await _context.Workers_Groups.AddRangeAsync(list);

			await _context.SaveChangesAsync();
			list.ForEach(wg => { _context.Entry(wg).State = EntityState.Detached; });
		}

		private async Task UpdateWorkerSchedule(WorkerVM workerVM)
		{

			//SCHEDULE
			var workerSchedules = await _context.Schedules.Where(s => s.WorkerId == workerVM.Id).ToListAsync();



			if (workerSchedules.Count != 0)
			{
				_context.Schedules.RemoveRange(workerSchedules);
				await _context.SaveChangesAsync();

			}


			var schedules = workerVM.Schedules.Where(s => s.Start != TimeSpan.Zero && s.End != TimeSpan.Zero).ToList();

			schedules.ForEach(w => _context.Entry(w).State = EntityState.Unchanged);

			if (schedules.Count > 0)
			{
				await _context.Schedules.AddRangeAsync(schedules);
				await _context.SaveChangesAsync();
			}

		}

		public async Task UpdateWorkerAsync(WorkerVM workerVM)
		{

			await UpdateWorkerInfo(workerVM);

			await UpdateWorkersGroups(workerVM);

			await UpdateWorkerSchedule(workerVM);


		}

		public async Task DeleteAsync(string workerId)
		{
			var worker = await _context.Users.Where(u => u.Id == workerId).FirstOrDefaultAsync();

			if (worker == null)
				throw new NullReferenceException("There is no worker with this user name!");

			_context.Users.Remove(worker);
			_context.SaveChanges();
		}

	}
}

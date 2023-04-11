using agSalon.Domain.Concrete;
using agSalon.Domain.Entities;
using agSalon.Domain.Entities.Enums;
using agSalon.Domain.Entities.Static;
using agSalon.Services.Services.Implementations;
using agSalon.Services.Services.Interfaces;
using agSalon.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace agSalon.Controllers
{
	public class WorkersController : Controller
	{
		private readonly IWorkersService _workersService;
		private readonly IGroupsService _groupsService;
		public WorkersController(IWorkersService workersService, IGroupsService groupsService)
		{
			_workersService = workersService;
			_groupsService = groupsService;
		}		

		public async Task<IActionResult> Index()
		{
			return View(await _workersService.GetWorkersAsync());
		}


		public async Task<IActionResult> Edit(string workerId)
		{
			var worker = await _workersService.GetWorkerVMByIdAsync(workerId);

			var groups = await _groupsService.GetAllAsync();
			ViewBag.Groups = new SelectList(groups, "Id", "Name");
			ViewBag.Days = Enum.GetValues(typeof(Days)).Cast<Days>().ToList();

			List<Schedule> schedules = new List<Schedule>();
			foreach (var day in ViewBag.Days)
			{
				schedules.Add(worker.Schedules.Where(w => w.Day == day.ToString()).FirstOrDefault());
				if (schedules.Last() == null)
					schedules[schedules.Count-1] = new Schedule();
			}
			
			worker.Schedules = schedules;

			return View(worker);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(WorkerVM workerVM)
		{
			if(!ModelState.IsValid)
			{
				var groups = await _groupsService.GetAllAsync();
				ViewBag.Groups = new SelectList(groups, "Id", "Name");
				ViewBag.Days = Enum.GetValues(typeof(Days)).Cast<Days>().ToList();
				return View(workerVM);
			}

			await _workersService.UpdateWorkerAsync(workerVM);
			return RedirectToAction(nameof(Index));
		}


	}
}

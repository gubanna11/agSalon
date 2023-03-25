using agSalon.Domain.Abstract;
using agSalon.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using agSalon.Services.Services;
using agSalon.Services.Services.Implementations;
using agSalon.Services.Services.Interfaces;

namespace agSalon.Controllers
{
	public class GroupsController : Controller
	{
		private readonly IUnitOfWork _unit;
		private readonly IGroupsService _groupsService;


		public GroupsController(IUnitOfWork unit, IGroupsService groupsService)
		{
			_unit = unit;
			_groupsService = groupsService;
		}

		public async Task<IActionResult> Index()
		{
			//var allGroups = await _unit.Repository<GroupOfServices>().GetAllAsync();
			var allGroups = await _groupsService.GetAllAsync();

			return View(allGroups);
		}

		public IActionResult Create()
		{
			return View();
		}



		[HttpPost]
		public async Task<IActionResult> Create(GroupOfServices newGroup)
		{
			//await _unit.Repository<GroupOfServices>().AddNewGroupAsync(newGroup);

			await _groupsService.AddNewGroupAsync(newGroup);

			await _unit.SaveChangesAsync();

			return RedirectToAction("Index");
		}


		public async Task<IActionResult> Edit(int groupId)
		{
			var group = await _groupsService.GetByIdAsync(groupId);

			return View(group);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(GroupOfServices group)
		{
			await _groupsService.UpdateGroupAsync(group);
			await _unit.SaveChangesAsync();

			return RedirectToAction("Index");
		}


		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _groupsService.DeleteGroupAsync(id);
			await _unit.SaveChangesAsync();

			return RedirectToAction("Index");
		}
	}
}

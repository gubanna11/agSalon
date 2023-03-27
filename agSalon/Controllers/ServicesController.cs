using agSalon.Domain.Abstract;
using agSalon.Domain.Concrete;
using agSalon.Domain.Entities;
using agSalon.Services.Services.Interfaces;
using agSalon.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace agSalon.Controllers
{
	public class ServicesController : Controller
	{
		private readonly IServicesService _service;
		private readonly IGroupsService _groupsService;
		private readonly IUnitOfWork _unitOfWork;

		public ServicesController(AppDbContext context, IServicesService service, IGroupsService groupsService, IUnitOfWork unitOfWork)
		{
			_service = service;
			_groupsService = groupsService;
			_unitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Index(int groupId)
		{
			var services = _service.GetServicesByGroupId(groupId);

			ViewBag.GroupName = (await _groupsService.GetByIdAsync(groupId)).Name;

			return View(services);
		}

		public async Task<IActionResult> Create()
		{
			var groups = await _groupsService.GetAllAsync();
			ViewBag.Groups = new SelectList(groups, "Id", "Name");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(ServiceVM newService)
		{
			var groups = await _groupsService.GetAllAsync();
			ViewBag.Groups = new SelectList(groups, "Id", "Name");
			try
			{
				if (!ModelState.IsValid)
				{
					return View(newService);
				}

				await _service.AddNewServiceAsync(newService);
			}
			catch (Exception)
			{
				ViewBag.Duplicate = "DUPLICATE NAME";
				return View(newService);
			}

			return Redirect("Index?groupId=" + newService.GroupId);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var service = await _service.GetServiceByIdWithGroupAsync(id);

			int groupId = service.Service_Group.GroupId;

			if (service != null)
			{
				await _service.DeleteAsync(id);
				await _unitOfWork.SaveChangesAsync();
			}

			return Redirect("~/Services/Index?groupId=" + groupId);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var service = await _service.GetServiceByIdWithGroupAsync(id);
			var groups = await _groupsService.GetAllAsync();
			ViewBag.Groups = new SelectList(groups, "Id", "Name");
			ViewBag.GroupName = service.Service_Group.Group.Name;

			ServiceVM serviceVM = new ServiceVM()
			{
				Id = id,
				Name = service.Name,
				Price = service.Price,
				GroupId = service.Service_Group.GroupId
			};
			return View(serviceVM);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ServiceVM serviceVM)
		{
			var groups = await _groupsService.GetAllAsync();
			ViewBag.Groups = new SelectList(groups, "Id", "Name");
			try
			{
				if (!ModelState.IsValid)
				{
					return View(serviceVM);
				}
				await _service.UpdateServiceAsync(serviceVM);
			}
			catch (Exception)
			{
				return View(serviceVM);
			}
			return Redirect("~/Services/Index?groupId=" + serviceVM.GroupId);
		}
	}
}

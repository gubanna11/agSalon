using agSalon.Domain.Concrete;
using agSalon.Domain.Entities.Static;
using agSalon.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using agSalon.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using agSalon.Services.Services.Interfaces;
using agSalon.Domain.Entities.Enums;

namespace agSalon.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<Client> _userManager;
		private readonly SignInManager<Client> _signInManager;
		private readonly AppDbContext _context;
		private readonly IGroupsService _groupsService;

		public AccountController(UserManager<Client> userManager, SignInManager<Client> signInManager, AppDbContext context, IGroupsService groupsService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_groupsService = groupsService;
		}

		public async Task<IActionResult> Users()
		{
			var users = await _context.Users.ToListAsync();
			return View(users);
		}

		public IActionResult Login()
		{
			return View(new LoginVM());
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM, string returnUrl)
		{
			if (!ModelState.IsValid)
				return View(loginVM);

			var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

			if (user != null)
			{
				var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

				if (passwordCheck)
				{
					var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

					if (result.Succeeded)
					{
						if (string.IsNullOrEmpty(returnUrl))
							return RedirectToAction("Index", "Home");
						return Redirect(returnUrl);
					}

				}
				TempData["Error"] = "Wrong credentials(password). Please, try again";
				return View(loginVM);
			}

			TempData["Error"] = "Wrong credentials. Please, try again";
			return View(loginVM);
		}

		public IActionResult Register() 
		{
			TempData["Role"] = UserRoles.Client;
			return View(new RegisterVM());
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM registerVM, string role = UserRoles.Client)
		{
			TempData["Role"] = role;
			if (!ModelState.IsValid)
				return View(registerVM);

			var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

			if (user != null)
			{
				TempData["Error"] = "this email address is already in use!";
				return View(registerVM);
			}

			var newUser = new Client()
			{
				Surname = registerVM.Surname,
				Name = registerVM.Name,
				PhoneNumber = registerVM.Phone,
				DateBirth = registerVM.DateBirth,
				Email = registerVM.EmailAddress,
				UserName = registerVM.EmailAddress
			};

			var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

			if (newUserResponse.Succeeded)
			{
				await _userManager.AddToRoleAsync(newUser, role);
				return View("RegisterCompleted");
			}

			ViewBag.Errors = newUserResponse.Errors;
			
			return View(registerVM);
		}

		[Authorize(Roles = UserRoles.Admin)]
		public IActionResult CreateAdmin() => View(new RegisterVM());

		[Authorize(Roles = UserRoles.Admin)]
		[HttpPost]
		public Task<IActionResult> CreateAdmin(RegisterVM registerVM)
		{
			return Register(registerVM, UserRoles.Admin);			
		}




		[Authorize(Roles = UserRoles.Admin)]
		public async Task<IActionResult> CreateWorker()
		{
			var groups = await _groupsService.GetAllAsync();
			ViewBag.Groups = new SelectList(groups, "Id", "Name");
			return View(new NewWorkerVM());
		}

		[Authorize(Roles = UserRoles.Admin)]
		[HttpPost]
		public async Task<IActionResult> CreateWorker(NewWorkerVM newWorkerVM)
		{
			var groups = await _groupsService.GetAllAsync();
			ViewBag.Groups = new SelectList(groups, "Id", "Name");

			if (!ModelState.IsValid)
			{
				return View(newWorkerVM);
			}


			var user = await _userManager.FindByEmailAsync(newWorkerVM.EmailAddress);

			if (user != null)
			{
				TempData["Error"] = "this email address is already in use!";
				return View(newWorkerVM);
			}

			var newUser = new Client()
			{
				Surname = newWorkerVM.Surname,
				Name = newWorkerVM.Name,
				PhoneNumber = newWorkerVM.Phone,
				DateBirth = newWorkerVM.DateBirth,
				Email = newWorkerVM.EmailAddress,
				EmailConfirmed = true,
				UserName = newWorkerVM.EmailAddress
			};

			var newUserResponse = await _userManager.CreateAsync(newUser, newWorkerVM.Password);

			if (newUserResponse.Succeeded)
			{
				await _userManager.AddToRoleAsync(newUser, UserRoles.Worker);
			}
			else if(newUserResponse.Errors.Count() > 0)
			{
				ViewBag.Errors = newUserResponse.Errors;

				return View(newWorkerVM);
			}

			newWorkerVM.Id = newUser.Id;

			var newWorker = new Worker()
			{
				Id = newWorkerVM.Id,
				Address = newWorkerVM.Address,
				Gender = newWorkerVM.Gender
			};

			_context.Workers.Add(newWorker);


			var list = new List<Worker_Group>();
			foreach (var groupId in newWorkerVM.GroupsIds)
			{
				Worker_Group worker_group = new Worker_Group
				{
					WorkerId = newWorkerVM.Id,
					GroupId = groupId
				};

				list.Add(worker_group);
			}

			await _context.Workers_Groups.AddRangeAsync(list);

			await _context.SaveChangesAsync();

			return View("RegisterCompleted");			
		}


		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}

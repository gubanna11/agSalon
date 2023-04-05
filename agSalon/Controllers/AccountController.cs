using agSalon.Domain.Concrete;
using agSalon.Domain.Entities.Static;
using agSalon.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using agSalon.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace agSalon.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<Client> _userManager;
		private readonly SignInManager<Client> _signInManager;
		private readonly AppDbContext _context;

		public AccountController(UserManager<Client> userManager, SignInManager<Client> signInManager, AppDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
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

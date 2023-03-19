using agSalon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace agSalon.Controllers
{
	public class HomeController : Controller
	{

		public HomeController()
		{
		}

		public IActionResult Index()
		{
			return View();
		}

	}
}
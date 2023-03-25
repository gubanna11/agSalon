using Microsoft.AspNetCore.Mvc;

namespace agSalon.Services.ViewComponents
{
	public class DeleteViewComponent : ViewComponent
	{
		public DeleteViewComponent()
		{

		}

		public IViewComponentResult Invoke(int id)
		{
			return View(id);
		}
	}
}

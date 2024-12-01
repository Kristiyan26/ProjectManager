using Microsoft.AspNetCore.Mvc;
using ProjectManager.ActionFilters;
using ProjectManager.Entities;
using ProjectManager.ExtentionMethods;
using ProjectManager.Repositories;
using ProjectManager.ViewModels.Home;
using System.Diagnostics;

namespace ProjectManager.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}


		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

            ProjectManagerDbContext context = new ProjectManagerDbContext();
			User loggedUser = context.Users.Where(u => u.Username == model.Username &&
															  u.Password == model.Password)
				                                                          .FirstOrDefault();

			if (loggedUser == null)
			{
				this.ModelState.AddModelError("authError", "Invalid username or password!");
				return View(model);

			}
			else
            {
                HttpContext.Session.SetObject("loggedUser", loggedUser);
            }


			return RedirectToAction("Index", "Home");
            
                
            

        }

		[AuthenticationFilter]
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("loggedUser");

			return RedirectToAction("Login", "Home");
        }

	

	}
	
}

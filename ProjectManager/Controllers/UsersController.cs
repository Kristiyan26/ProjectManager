using Microsoft.AspNetCore.Mvc;
using ProjectManager.ActionFilters;
using ProjectManager.Entities;
using ProjectManager.Repositories;
using ProjectManager.ViewModels.Users;
using System.Globalization;

namespace ProjectManager.Controllers
{
    [AuthenticationFilter]
    public class UsersController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            ProjectManagerDbContext context = new ProjectManagerDbContext();

            IndexVM model = new IndexVM();
            model.Items = context.Users.ToList();
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            CreateVM model = new CreateVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ProjectManagerDbContext context = new ProjectManagerDbContext();
            User item = new User();


            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Username = model.Username;
            item.Password = model.Password;

            context.Users.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ProjectManagerDbContext context = new ProjectManagerDbContext();

            User item = new User();
            item.Id = id;


            context.Users.Remove(item);
            context.SaveChanges();


            return RedirectToAction("Index", "Users");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProjectManagerDbContext context = new ProjectManagerDbContext();
            User item = context.Users.Where(x => x.Id == id).FirstOrDefault();
            if(item == null)
            {
                return RedirectToAction("Index", "Users");
            }

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;
            return View(model);

        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ProjectManagerDbContext context = new ProjectManagerDbContext();


            User item = new User();

         
            item.Id=model.Id;
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            context.Users.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
                
        }
    }
}
    
    


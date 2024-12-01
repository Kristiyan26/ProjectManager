﻿using Microsoft.AspNetCore.Mvc;
using ProjectManager.ActionFilters;
using ProjectManager.Entities;
using ProjectManager.ExtentionMethods;
using ProjectManager.Repositories;
using ProjectManager.ViewModels.Projects;

namespace ProjectManager.Controllers
{

    [AuthenticationFilter]
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");
            ProjectManagerDbContext context = new ProjectManagerDbContext();
            IndexVM model = new IndexVM();

         
            model.Projects = context.Projects.Where(p=>p.OwnerId ==loggedUser.Id ).ToList();


            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            CreateVM model = new CreateVM();
            model.OwnerId= loggedUser.Id;


            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            if(model.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impresonation attempt detected!");
                return View(model);
            }


            ProjectManagerDbContext context=new ProjectManagerDbContext();
            Project item = new Project();
            item.OwnerId = loggedUser.Id;
            item.Title= model.Title;    
            item.Description= model.Description;
            
            context.Projects.Add(item);
            context.SaveChanges();


            return RedirectToAction("Index","Projects");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");
                

            ProjectManagerDbContext context = new ProjectManagerDbContext();

            Project project = context.Projects.Where(p => p.Id == id).FirstOrDefault();

            if (project == null) 
            {
                //todo: add shared owners    
                return RedirectToAction("Index", "Projects"); 
            }       

            if(project.OwnerId != loggedUser.Id)
            {
                return RedirectToAction("Index", "Projects");
            }

            EditVM model = new EditVM();

            model.Id=project.Id;
            model.OwnerId =project.OwnerId;
            model.Title = project.Title;
            model.Description = project.Description;

            return View(model);
        }

        [HttpPost]

        public IActionResult Edit(EditVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");
            ProjectManagerDbContext context = new ProjectManagerDbContext();

            Project item = context.Projects.Where(p => p.Id == model.Id).FirstOrDefault();



            if (item.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impresonation attempt detected!");
                return View(model);
            }           
            if(model.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impresonation attempt detected!");
                return View(model);
            }

            item.Id = model.Id;
            item.OwnerId= model.OwnerId;    
            item.Title = model.Title;
            item.Description = model.Description;

            context.Projects.Update(item);
            context.SaveChanges();


            return RedirectToAction("Index", "Projects");
        }
    }
}
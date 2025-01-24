using COMP2139_ICE.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP2139_ICE.Controllers
{
    public class ProjectController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Initialize the list and add projects
            var projects = new List<Project>
            {
                new Project { ProjectId = 1, Name = "Project 1", Description = "My First Project 1" },
            };

            return View(projects); // Pass the list to the view
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            // Normally, you would persist the project to a database here.
            // Redirect back to the Index action after adding a project.
            return RedirectToAction("Index");
            
            
        }
        
        [HttpGet]
        public IActionResult Details(int id)
        {
            // Mock data for demonstration; replace with database call
            var project = new Project
            {
                ProjectId = id,
                Name = "Sample Project",
                Description = "This is a sample project."
            };

            // Pass the project to the view
            return View(project);
        }
    }
}
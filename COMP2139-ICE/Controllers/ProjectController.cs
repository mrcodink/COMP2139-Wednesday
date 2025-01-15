using COMP2139_ICE.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP2139_ICE.Controllers;

public class ProjectController : Controller
{
  [HttpGet] // 
  public IActionResult Index()
  {
   var projects = new List<Project>();
   {
       new Project {ProjectId = 1, Name = "Project 1", Description = "My First Project 1"};
   }
   return View(projects);
  }

  [HttpGet]    
  public IActionResult Create()
  {
      return View();
  }
  
  
  [HttpPost]    
  public IActionResult Create(Project project)
  {
      return View();
  }
  
}







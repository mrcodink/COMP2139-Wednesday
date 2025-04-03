using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Models;
using COMP2139_Labs.Areas.ProjectManagement.Controllers;
using Microsoft.Extensions.Logging;

namespace COMP2139_Labs.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Accessed HomeController Index at {Time}", DateTime.Now);
        return View();
    }

    public IActionResult About()
    {
        _logger.LogInformation("Accessed HomeController About at {Time}", DateTime.Now);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("Accessed HomeController Error at {Time}", DateTime.Now);
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
    [HttpGet]
    public IActionResult GeneralSearch(string searchType, string searchString)
    {
        _logger.LogInformation("Accessed HomeController GeneralSearch at {Time}", DateTime.Now);
        searchType = searchType?.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(searchType) || string.IsNullOrWhiteSpace(searchString))
        {
            return RedirectToAction(nameof(Index), "Home");
        }
        
        if (searchType == "projects")
        {
            return RedirectToAction("Search", "Project", new { area = "ProjectManagement", searchString });
        }

        else if (searchType == "tasks")
        {
            return RedirectToAction("Search", "ProjectTask", new { area = "ProjectManagement", searchString });
            
        }
        
        return RedirectToAction(nameof(Index), "Home");
    }

    public IActionResult NotFound(int statusCode)
    {
        _logger.LogWarning("NotFound invoked at {Time}", DateTime.Now);
        if (statusCode == 404)
        {
            return View("NotFound");
        }

        return View("Error");
    }
 
}
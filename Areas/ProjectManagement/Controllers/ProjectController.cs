using COMP2139_Labs.Data;
using COMP2139_Labs.Models; 
using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.PostgresTypes;

namespace COMP2139_Labs.Areas.ProjectManagement.Controllers;

[Area("ProjectManagement")]
[Route("[area]/[controller]/[action]")]
public class ProjectController : Controller
{
    private readonly ILogger<ProjectController> _logger;
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context, ILogger<ProjectController> logger)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet("")] // Get is Default
    public async Task<IActionResult> Index()
    {
        // Retrieve all Proj form database
        var projects = await _context.Projects.ToListAsync();
        return View(projects);
    }

    // Displays the form ot create a new project
    [HttpGet("Create")]
    public IActionResult Create()
    {
        _logger.LogInformation("Accessed ProjectController Index at {Time}", DateTime.Now);
        return View();
    }
    
    // handles submission of create form to add new project
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Project project)
    {
        _logger.LogInformation("Accessed ProjectController Index at {Time}", DateTime.Now);
        if (ModelState.IsValid)
        {
            project.ProjectStartDate = DateTime.SpecifyKind(project.ProjectStartDate, DateTimeKind.Utc);
            project.ProjectEndDate = DateTime.SpecifyKind(project.ProjectEndDate, DateTimeKind.Utc);
            _context.Projects.Add(project); // Add new project to database
            await _context.SaveChangesAsync(); // Save Changes to database
            return RedirectToAction("Index");
        }
        //Database --> Persist new project to the database
        return View(project); 
    }
    
    
    // CRUD - Create - Read - Update - Delete
    // Details view if project exists otherwise notfound
    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        _logger.LogInformation("Accessed ProjectController Index at {Time}", DateTime.Now);
        
        //var project = new Project { ProjectId = id, ProjectName = "Project 1", ProjectDescription = "First Project"};
        // Retrieves the project with the specified id or returns null if not found
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
        if (project == null)
        {
            _logger.LogWarning("Could not find Project with id of {id}", id);
            return NotFound(); // 404 error
        }
        return View(project);
    }

    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        // Finds the project by its primary key
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound(); // returns a 404 error if project doesn't exist
        }
        return View(project);
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectName,ProjectDescription")] Project project)
    {
        // [BIND] ensures only the specified properties are updated for security reasons
        if (id != project.ProjectId)
        {
            return NotFound();  // Ensures the ID in the route matches the ID in the model
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Projects.Update(project); // updates the project in the database
                await _context.SaveChangesAsync(); // Saves the changes to database
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handles concurrency issues where another process modifies the project simultaneously
                if (!await ProjectExists(project.ProjectId))
                {
                    return NotFound(); // Returns a 404 if project no longer exists
                }
                else
                {
                    throw; // Re-throws the exception if issues is unknown
                }
                
            }
            return RedirectToAction("Index");
        }
        return View(project);
    }

    
    private async Task<bool> ProjectExists(int id)
    {
        return await _context.Projects.AnyAsync(e => e.ProjectId == id);
    }

    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Retrieves the project with the specified id or returns null if not found
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost("DeleteConfirmed/{projectId:int}"), ActionName("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(int projectId)
    {
        // Finds the project by its primary key
        var project = await _context.Projects.FindAsync(projectId);
        if (project != null)
        {
            _context.Projects.Remove(project); // Remove project from database
            await _context.SaveChangesAsync();            // Commit changes to database
            return RedirectToAction("Index");
        }
        return NotFound();  
    }


    [HttpGet("Search/{searchString?}")]
    public async Task<IActionResult> Search(string searchString)
    {
        var projectsQuery = _context.Projects.AsQueryable();

        bool searchPerformed = !string.IsNullOrWhiteSpace(searchString);

        if (searchPerformed)
        {
            searchString = searchString.ToLower();

            projectsQuery = projectsQuery.Where(p => p.ProjectName.ToLower().Contains(searchString) ||
                                                     (p.ProjectDescription != null && p.ProjectDescription.ToLower()
                                                         .Contains(searchString)));


        }
        
        // Asynchronous execution means this method does not block the thread while waiting for the database
        var projects = await projectsQuery.ToListAsync();
        
        // Pass search metadata
        ViewData["SearchString"] = searchString;
        ViewData["SearchPerformed"] = searchPerformed;
            
        return View("Index", projects);

    }
    
    
}
using COMP2139_Labs.Areas.ProjectManagement.Models;
using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Areas.ProjectManagement.Controllers;

[Area("ProjectManagement")]
[Route("[area]/[controller]/[action]")]
public class ProjectCommentController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectCommentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetComments(int projectId)
    {
        // Query the database for comments related to the project
        // ordered by the most recent posting date
        var comments = await _context.ProjectComments
            .Where(c => c.ProjectId == projectId)
            .OrderByDescending(c => c.DatePosted)
            .ToListAsync();

        return Json(comments);
    }


    public async Task<IActionResult> AddComment([FromBody] ProjectComment comment)
    {
        if (ModelState.IsValid)
        {
            // Current date and time of the comment
            comment.DatePosted = DateTime.Now;
            
            //Save the comment
            _context.ProjectComments.Add(comment);
            
            // Commit to database
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Comment successfully added." });
        }
        
        var errors = ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage);
        return Json(new { success = false, message = "Invalid comment.", errors = errors });
    }
}
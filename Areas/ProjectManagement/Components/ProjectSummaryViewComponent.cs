using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Areas.ProjectManagement.Components;

public class ProjectSummaryViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public ProjectSummaryViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int projectId)
    {
        var project = await _context.Projects
            .Include(p => p.Tasks) // load related tasks for the project (eager laoding)
            .FirstOrDefaultAsync(p => p.ProjectId == projectId); // retrieve the project with the matching ID

        if (project == null)
        {
            return Content("Project not found");
        }
        return View(project);
    }
}
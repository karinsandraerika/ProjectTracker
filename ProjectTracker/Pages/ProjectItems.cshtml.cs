using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Models;

namespace ProjectTracker.Pages;

public class ProjectItemsModel : PageModel
{
    private readonly ProjectTracker.Data.DatabaseContext _context;

    public ProjectItemsModel(ProjectTracker.Data.DatabaseContext context)
    {
        _context = context;
    }

    public List<ProjectItem> ProjectItems { get; set; } = default!;


    public void OnGet()
    {
        ProjectItems = _context.ProjectItem.ToList();
    }
}

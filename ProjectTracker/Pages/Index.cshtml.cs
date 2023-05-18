using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Models;

namespace ProjectTracker.Pages;

public class IndexModel : PageModel
{
    private readonly DatabaseContext _context;
 

    public IndexModel(DatabaseContext context)
    {
        _context = context;

    }

    public List<Project> Projects { get; set; } = default!;


    public void OnGet()
    {
        Projects = _context.Project.ToList();
    }


    public ActionResult OnPostDelete(int id)
    {
        Project projectToDelete = _context.Project.SingleOrDefault(p => p.Id == id);
        _context.Project.Remove(projectToDelete);
        _context.SaveChanges();
        return RedirectToPage("/Index");
    }

    public Dictionary<string, double> ProjectProgress(int id)
    {
        var project = _context.Project.Include(proj => proj.ProjectItems).SingleOrDefault(p => p.Id == id);

        Dictionary<string, double> progress = new Dictionary<string, double>
        {
            { "Not Started", 0 },
            { "In Progress", 0},
            { "Completed", 0 }
        };

        if (project.ProjectItems == null)
        {
            return progress;
        }

        int totalHours = 0;
        int notStarted = 0;
        int inProgress = 0;
        int completed = 0;

        foreach (var item in project.ProjectItems)
        {
            totalHours += item.HoursToComplete ?? 0; // Use null-coalescing operator to handle nullable integers
            switch (item.Completed)
            {
                case Enums.CompletionStatus.NotStarted:
                    notStarted += item.HoursToComplete ?? 0;
                    break;
                case Enums.CompletionStatus.InProgress:
                    inProgress += item.HoursToComplete ?? 0;
                    break;
                case Enums.CompletionStatus.Completed:
                    completed += item.HoursToComplete ?? 0;
                    break;
                default:
                    break;
            }
        }
        if (totalHours == 0)
        {
            return progress;
        }

        progress["Not Started"] = (double)notStarted / totalHours * 100;
        progress["In Progress"] = (double)inProgress / totalHours * 100;
        progress["Completed"] = (double)completed / totalHours * 100;


        return progress;
    }
}

     
          
      


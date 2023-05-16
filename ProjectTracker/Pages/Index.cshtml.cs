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

    //[BindProperty]
    //public Project Project { get; set; } = default!;


    //public ActionResult OnPost()
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Project.Add(Project);
    //        _context.SaveChanges();
    //        return RedirectToPage("/Index");
    //    }
    //    return Page();
    //}

    public ActionResult OnPostDelete(int id)
    {
        Project projectToDelete = _context.Project.SingleOrDefault(p => p.Id == id);
        _context.Project.Remove(projectToDelete);
        _context.SaveChanges();
        return RedirectToPage("/Index");
    }

    //public ActionResult OnPostAddProject()
    //{
    //    return RedirectToPage("/CreateProject");
    //}


}

     
          
      


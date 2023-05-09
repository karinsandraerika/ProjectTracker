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

}

     
          
      


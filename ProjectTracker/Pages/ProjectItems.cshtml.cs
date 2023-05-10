using System;
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
    public List<SelectListItem> PersonListItems { get; set; }

    [BindProperty]
    public ProjectItem projectItem { get; set; }


    public void OnGet()
    {
        ProjectItems = _context.ProjectItem.Include(projectId => projectId.Project).
            Include(person => person.Persons).ToList();
        var persons = _context.Person.ToList();
        PersonListItems = persons.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.Name
        }).ToList();
        
    }

    public ActionResult OnPost()
    {
        string[] selectedPersonIds = Request.Form["selectedPersons"];

        projectItem.Persons = new List<Person>();
        foreach (string personId in selectedPersonIds)
        {
            var person = _context.Person.Find(int.Parse(personId));
            if (person != null)
            {
                projectItem.Persons.Add(person);
            }
        }

        if (ModelState.IsValid)
        {
            _context.ProjectItem.Add(projectItem);
            _context.SaveChanges();
        }
        return RedirectToPage("./ProjectItems");
    }

    

}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Enums;
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

    public Importance SelectedImportance { get; set; }
    public List<SelectListItem> ImportanceOptions { get; set; }

    public CompletionStatus SelectedCompletionStatus { get; set; }
    public List<SelectListItem> CompletionOptions { get; set; }

    public int projectId;

    public void OnGet(int id)
    {
        projectId = id;


        // select all ProjectItems where Project Id = id
        // Include Project and Persons list 
        ProjectItems = _context.ProjectItem
            .Include(pItems => pItems.Project).Include(pItems => pItems.Persons).ToList();


        // Create lists for dropdown boxes, for persons and enums
        var persons = _context.Person.ToList();

        PersonListItems = persons.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.Username
        }).ToList();

        ImportanceOptions = Enum.GetValues(typeof(Importance))
                                .Cast<Importance>()
                                .Select(i => new SelectListItem
                                {
                                    Value = i.ToString(),
                                    Text = i.ToString()
                                })
                                .ToList();
        CompletionOptions = Enum.GetValues(typeof(CompletionStatus))
                                .Cast<CompletionStatus>()
                                .Select(i => new SelectListItem
                                {
                                    Value = i.ToString(),
                                    Text = i.ToString()
                                })
                                .ToList();
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
            if (Enum.TryParse(Request.Form["SelectedCompletionStatus"], out CompletionStatus completionStatus))
            {
                projectItem.Completed = completionStatus;
            }
            if (Enum.TryParse(Request.Form["SelectedImportance"], out Importance importance))
            {
                projectItem.Importance = importance;
            }

            projectItem.Project = _context.Project.SingleOrDefault(p => p.Id == projectId);

            _context.ProjectItem.Add(projectItem);
            _context.SaveChanges();
        }
        return RedirectToPage("./ProjectItems", new { id = projectId });
    }

    public ActionResult OnPostDelete(int id)
    {
        ProjectItem itemToDelete = _context.ProjectItem.SingleOrDefault(p => p.Id == id);
        _context.ProjectItem.Remove(itemToDelete);
        _context.SaveChanges();
        return RedirectToPage("/ProjectItems");
    }
}

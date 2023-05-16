using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Enums;
using ProjectTracker.Models;

namespace ProjectTracker.Pages
{
    public class CreateProjectItemModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public CreateProjectItemModel(ProjectTracker.Data.DatabaseContext context)
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

        [BindProperty]
        public int projectId { get; set; }

        public void OnGet(int id)
        {
            projectId = id;

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

            projectItem.Project = _context.Project.FirstOrDefault(p => p.Id == projectId);

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

                _context.ProjectItem.Add(projectItem);
                _context.SaveChanges();
            }
            return RedirectToPage("./ProjectDetails", new { id = projectId });
        }
    }
}


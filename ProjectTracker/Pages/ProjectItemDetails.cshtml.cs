using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Enums;
using ProjectTracker.Models;

namespace ProjectTracker.Pages
{
    public class ProjectItemDetailsModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public ProjectItemDetailsModel(ProjectTracker.Data.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.ProjectItem ProjectItem { get; set; } = default!;

        [BindProperty]
        public int projectId { get; set; }

        public List<Models.ProjectItem> ProjectItems { get; set; } = default!;
        public List<SelectListItem> PersonListItems { get; set; }

        public Importance SelectedImportance { get; set; }
        public List<SelectListItem> ImportanceOptions { get; set; }

        public CompletionStatus SelectedCompletionStatus { get; set; }
        public List<SelectListItem> CompletionOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var projectitem = await _context.ProjectItem.Include(pItems => pItems.Project)
               .Include(pItems => pItems.Persons).FirstOrDefaultAsync(m => m.Id == id);

            if (projectitem == null)
            {
                return NotFound();
            }

            if (id == null || _context.ProjectItem == null)
            {
                return NotFound();
            }

            ProjectItem = projectitem;

            projectId = projectitem.Project.Id;

            // Create lists for dropdown boxes, for persons and enums

            var persons = ProjectItem.Persons.ToList();

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
            
            
            return Page();
        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Enum.TryParse(Request.Form["SelectedCompletionStatus"], out CompletionStatus completionStatus))
            {
                ProjectItem.Completed = completionStatus;
            }
            if (Enum.TryParse(Request.Form["SelectedImportance"], out Importance importance))
            {
                ProjectItem.Importance = importance;
            }

            _context.Attach(ProjectItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectItemExists(ProjectItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return RedirectToPage("/ProjectDetails", new { id = projectId });
        }

        private bool ProjectItemExists(int id)
        {
            return (_context.ProjectItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }


    //private readonly ProjectTracker.Data.DatabaseContext _context;

    //public ProjectItemDetailsModel(ProjectTracker.Data.DatabaseContext context)
    //{
    //    _context = context;
    //}

    //[BindProperty]
    //public ProjectItem ProjectItem { get; set; } = default!;

    //public async Task<IActionResult> OnGetAsync(int? id)
    //{
    //    if (id == null || _context.ProjectItem == null)
    //    {
    //        return NotFound();
    //    }

    //    var projectitem = await _context.ProjectItem.Include(pItems => pItems.Project)
    //        .Include(pItems => pItems.Persons).FirstOrDefaultAsync(m => m.Id == id);
    //    if (projectitem == null)
    //    {
    //        return NotFound();
    //    }
    //    ProjectItem = projectitem;
    //    return Page();
    //}


}



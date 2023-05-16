using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Models;

namespace ProjectTracker.Pages
{
    public class ProjectDetailsModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public ProjectDetailsModel(ProjectTracker.Data.DatabaseContext context)
        {
            _context = context;
        }
        public List<Models.ProjectItem> ProjectItems { get; set; } = default!;
        public Models.Project project { get; set; }

        [BindProperty]
        public int ProjectId { get; set; }

        public List<Person> persons { get; set; }


        public void OnGet(int id)
        {
            project = _context.Project.SingleOrDefault(p => p.Id == id);
            ProjectId = id;

            // select all ProjectItems where Project Id = id
            // Include Persons id list 
            ProjectItems = _context.ProjectItem.Where(pItem => pItem.Project.Id == id)
                .Include(pItems => pItems.Persons).ToList();
            
            
        }

        public ActionResult OnPostDelete(int id)
        {
            Models.ProjectItem itemToDelete = _context.ProjectItem.SingleOrDefault(p => p.Id == id);
            _context.ProjectItem.Remove(itemToDelete);
            _context.SaveChanges();
            return RedirectToPage("/ProjectDetails");
        }

        //public ActionResult OnPostAddItem()
        //{
        //    return RedirectToPage("./CreateProjectItem", new { id = ProjectId });
        //}
    }

}

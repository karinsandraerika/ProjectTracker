using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectTracker.Data;
using ProjectTracker.Models;

namespace ProjectTracker.Pages
{
    public class CreateProjectModel : PageModel
    {
        private readonly DatabaseContext _context;

        public CreateProjectModel(DatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }


        [BindProperty]
        public Project Project { get; set; } = default!;


        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Project.Add(Project);
                _context.SaveChanges();
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
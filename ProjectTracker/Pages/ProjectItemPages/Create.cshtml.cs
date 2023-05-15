using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTracker.Data;
using ProjectTracker.Models;

namespace ProjectTracker.Pages.ProjectItemPages
{
    public class CreateModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public CreateModel(ProjectTracker.Data.DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProjectItem ProjectItem { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ProjectItem == null || ProjectItem == null)
            {
                return Page();
            }

            _context.ProjectItem.Add(ProjectItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

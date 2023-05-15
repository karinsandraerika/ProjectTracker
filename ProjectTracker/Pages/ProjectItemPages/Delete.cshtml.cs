using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Models;

namespace ProjectTracker.Pages.ProjectItemPages
{
    public class DeleteModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public DeleteModel(ProjectTracker.Data.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ProjectItem ProjectItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ProjectItem == null)
            {
                return NotFound();
            }

            var projectitem = await _context.ProjectItem.FirstOrDefaultAsync(m => m.Id == id);

            if (projectitem == null)
            {
                return NotFound();
            }
            else 
            {
                ProjectItem = projectitem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ProjectItem == null)
            {
                return NotFound();
            }
            var projectitem = await _context.ProjectItem.FindAsync(id);

            if (projectitem != null)
            {
                ProjectItem = projectitem;
                _context.ProjectItem.Remove(ProjectItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

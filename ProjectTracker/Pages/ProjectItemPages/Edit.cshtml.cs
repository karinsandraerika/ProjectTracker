using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Models;

namespace ProjectTracker.Pages.ProjectItemPages
{
    public class EditModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public EditModel(ProjectTracker.Data.DatabaseContext context)
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

            var projectitem =  await _context.ProjectItem.FirstOrDefaultAsync(m => m.Id == id);
            if (projectitem == null)
            {
                return NotFound();
            }
            ProjectItem = projectitem;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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

            return RedirectToPage("./Index");
        }

        private bool ProjectItemExists(int id)
        {
          return (_context.ProjectItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

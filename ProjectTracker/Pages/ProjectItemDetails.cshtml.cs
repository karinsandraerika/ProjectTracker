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
        public ProjectItem ProjectItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ProjectItem == null)
            {
                return NotFound();
            }

            var projectitem = await _context.ProjectItem.Include(pItems => pItems.Project)
                .Include(pItems => pItems.Persons).FirstOrDefaultAsync(m => m.Id == id);
            if (projectitem == null)
            {
                return NotFound();
            }
            ProjectItem = projectitem;
            return Page();
        }

        
      

        private bool ProjectItemExists(int id)
        {
            return (_context.ProjectItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}


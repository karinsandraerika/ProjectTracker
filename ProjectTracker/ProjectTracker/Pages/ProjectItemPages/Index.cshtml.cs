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
    public class IndexModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public IndexModel(ProjectTracker.Data.DatabaseContext context)
        {
            _context = context;
        }

        public IList<ProjectItem> ProjectItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ProjectItem != null)
            {
                ProjectItem = await _context.ProjectItem.ToListAsync();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Models;

namespace ProjectTracker.Pages
{
	public class PersonDetailsModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public PersonDetailsModel(ProjectTracker.Data.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.Include(p => p.Projects)
                .Include(p => p.ProjectItems).FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            Person = person;
            return Page();
        }





    }
}


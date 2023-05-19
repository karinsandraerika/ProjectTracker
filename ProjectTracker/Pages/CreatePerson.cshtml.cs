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
    public class CreatePersonModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public CreatePersonModel(ProjectTracker.Data.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Person person { get; set; }

        public void OnGet()
        {

        }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Person.Add(person);
                _context.SaveChanges();
                return RedirectToPage("./Persons");
            }
            return Page();
        }

        public ActionResult OnPostDelete(int id)
        {
            Person personToDelete = _context.Person.SingleOrDefault(p => p.Id == id);
            _context.Person.Remove(personToDelete);
            _context.SaveChanges();
            return RedirectToPage("/Persons");
        }
    }
}
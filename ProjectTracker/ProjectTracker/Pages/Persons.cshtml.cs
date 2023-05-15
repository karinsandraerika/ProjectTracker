using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Models;

namespace ProjectTracker.Pages;

    public class PersonsModel : PageModel
    {
        private readonly ProjectTracker.Data.DatabaseContext _context;

        public PersonsModel(ProjectTracker.Data.DatabaseContext context)
        {
            _context = context;
        }

        public List<Person> Persons { get; set; } = default!;
        [BindProperty]
        public Person person { get; set; }

        public void OnGet()
        {
            Persons = _context.Person.Include(items => items.ProjectItems).ToList();
        }

        
        public ActionResult OnPost() 
        {
            if (ModelState.IsValid) 
            {
                _context.Person.Add(person); 
                _context.SaveChanges();
                return RedirectToPage("./Persons");
            }
            return RedirectToPage("./Persons");
    }

        public ActionResult OnPostDelete(int id)
        {
            Person personToDelete = _context.Person.SingleOrDefault(p => p.Id == id);
            _context.Person.Remove(personToDelete);  
            _context.SaveChanges();
            return RedirectToPage("/Persons");
        }
    }

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


        public void OnGet()
        {
            Persons = _context.Person.ToList();
        }
    }

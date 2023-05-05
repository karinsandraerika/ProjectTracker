using System;
using ProjectTracker.Data;
using ProjectTracker.Interfaces;
using ProjectTracker.Models;

namespace ProjectTracker.Repository
{
	public class PersonRepository : IPersonRepository
	{
        private readonly DatabaseContext _context;

        public PersonRepository(DatabaseContext context)
		{
            _context = context;
        }

        public ICollection<Person> GetPersons()
        {
            return _context.Person.ToList();
        }
    }
}


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

        public Person GetPerson(int id)
        {
            return _context.Person.Where(pe => pe.Id == id).FirstOrDefault();
        }

        public ICollection<Person> GetPersons()
        {
            return _context.Person.ToList();
        }

        public bool PersonExists(int id)
        {
            return _context.Person.Any(p => p.Id == id);
        }

        public bool CreatePerson(Person person)
        {
            _context.Add(person);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}


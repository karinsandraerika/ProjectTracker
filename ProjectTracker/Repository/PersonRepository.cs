using System;
using Microsoft.EntityFrameworkCore;
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
            // Include the list of projectitems (which will also include the list of persons inside projectitems).
            //return _context.Person.Include(pe => pe.ProjectItems).ToList();
            return _context.Person.ToList();
        }

        /*
        public Person GetPerson(string username)
        {
            return _context.Person.Where(pe => pe.Username == username).FirstOrDefault();

        }
        */

        public bool PersonExists(int id)
        {
            return _context.Person.Any(p => p.Id == id);
        }

        public bool CreatePerson(Person person)
        {
            _context.Add(person);
            return Save();
        }

        public bool UpdatePerson(Person person)
        {
            _context.Update(person);
            return Save();
        }

        public bool DeletePerson(Person person)
        {
            _context.Remove(person);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}


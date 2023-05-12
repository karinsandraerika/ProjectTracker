using System;
using System.Diagnostics.Metrics;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IPersonRepository
	{
        ICollection<Person> GetPersons();
        Person GetPerson(int id);
        bool PersonExists(int id);
        bool CreatePerson(Person person);
        bool UpdatePerson(Person person);
        bool DeletePerson(Person person);
        bool Save();
    }
}


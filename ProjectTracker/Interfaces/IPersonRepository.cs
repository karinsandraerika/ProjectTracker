using System;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IPersonRepository
	{
        ICollection<Person> GetPersons();
        Person GetPerson(int id);
        bool PersonExists(int id);
        bool CreatePerson(Person person);
        bool Save();
    }
}


using System;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IPersonRepository
	{
        ICollection<Person> GetPersons();
    }
}


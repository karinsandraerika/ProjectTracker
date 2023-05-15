using System;
using System.Diagnostics.Metrics;
using ProjectTracker.Dto;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IPersonRepository
	{
        ICollection<PersonDto> GetPersons();
        PersonDto GetPerson(int id);
        bool PersonExists(int id);
        bool CreatePerson(PersonDto person);
        bool UpdatePerson(PersonDto person);
        bool DeletePerson(Person person);
        bool Save();
    }
}


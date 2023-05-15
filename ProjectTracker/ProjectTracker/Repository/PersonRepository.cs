using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Dto;
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
/*
        public Person GetPerson(int id)
        {
            return _context.Person.Where(pe => pe.Id == id).FirstOrDefault();
        }
*/

        public PersonDto GetPerson(int id)
        {
            Person? person = _context.Person.Include(pe => pe.ProjectItems).Include(pe => pe.Projects).FirstOrDefault(pe => pe.Id == id);
            PersonDto personDto = new PersonDto()
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Username = person.Username,
                PhoneNumber = person.PhoneNumber
            };
            if (person.ProjectItems != null)
            {
                List<int> projectItemsIds = person.ProjectItems.Select(pi => pi.Id).ToList();
                personDto.ProjectItems = projectItemsIds;
            }
            if (person.Projects != null)
            {
                List<int> projectIds = person.Projects.Select(pi => pi.Id).ToList();
                personDto.Projects = projectIds;
            }
            return personDto;
        }

        public ICollection<PersonDto> GetPersons()
        {
            // Include the list of projectitems (which will also include the list of persons inside projectitems).
            var persons = _context.Person.Include(pe => pe.ProjectItems).Include(pe => pe.Projects).ToList();
            List <PersonDto> personDtos = new List<PersonDto>();
            foreach (var person in persons)
            {
                var personDto = new PersonDto()
                {
                    Id = person.Id,
                    Name = person.Name,
                    Email = person.Email,
                    Username = person.Username,
                    PhoneNumber = person.PhoneNumber
                };
                if (person.ProjectItems != null)
                {
                    List<int> projectItemsIds = person.ProjectItems.Select(pi => pi.Id).ToList();
                    personDto.ProjectItems = projectItemsIds;
                }
                if (person.Projects != null)
                {
                    List<int> projectIds = person.Projects.Select(pi => pi.Id).ToList();
                    personDto.Projects = projectIds;
                }
                personDtos.Add(personDto);
            }
            return personDtos;
            //return _context.Person.ToList();
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

        public bool CreatePerson(PersonDto newPerson)
        {
            var person = new Person()
            {
                Id = newPerson.Id,
                Email = newPerson.Email,
                Name = newPerson.Name,
                PhoneNumber = newPerson.PhoneNumber,
                Username = newPerson.Username
            };

            if (newPerson.ProjectItems != null)
            {
                var projectItems = _context.ProjectItem.Where(pi => newPerson.ProjectItems.Contains(pi.Id)).ToList();
                person.ProjectItems = projectItems;
            }
            if (newPerson.Projects != null)
            {
                var projects = _context.Project.Where(pi => newPerson.Projects.Contains(pi.Id)).ToList();
                person.Projects = projects;
            }
           
            _context.Add(person);
            return Save();
        }

        public bool UpdatePerson(PersonDto personInfo)
        {
            var person = _context.Person.Include(pe => pe.ProjectItems).Include(pe => pe.Projects).
                FirstOrDefault(pe => pe.Id == personInfo.Id);
            if (person == null)
            {
                return false;
            }
            if (personInfo.ProjectItems != null)
            {
                var projectItems = _context.ProjectItem.Where(pi => personInfo.ProjectItems.Contains(pi.Id)).ToList();
                person.ProjectItems = projectItems;
            }
            if (personInfo.Projects != null)
            {
                var projects = _context.Project.Where(pi => personInfo.Projects.Contains(pi.Id)).ToList();
                person.Projects = projects;
            }

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


﻿using System;
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

        public PersonDto GetPerson(int id)
        {
            Person? person = _context.Person.Include(pe => pe.ProjectItems).Include(pe => pe.Projects).FirstOrDefault(pe => pe.Id == id);
            PersonDto personDto = new PersonDto()
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Username = person.Username,
                PhoneNumber = person.PhoneNumber,
                ProjectItems = person.ProjectItems != null ? person.ProjectItems.Select(p => p.Id).ToList() : null,
                Projects = person.Projects != null ? person.Projects.Select(p => p.Id).ToList() : null
            };
            return personDto;
        }

        
        public ICollection<PersonDto> GetPersons()
        {
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
                    PhoneNumber = person.PhoneNumber,
                    ProjectItems = person.ProjectItems != null ? person.ProjectItems.Select(p => p.Id).ToList() : null,
                    Projects = person.Projects != null ? person.Projects.Select(p => p.Id).ToList() : null
                };
                personDtos.Add(personDto);
            }
            return personDtos;
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
                Username = newPerson.Username,
                ProjectItems = newPerson.ProjectItems != null ? _context.ProjectItem.Where(pi => newPerson.ProjectItems.Contains(pi.Id)).ToList() : null,
                Projects = newPerson.Projects != null ? _context.Project.Where(pi => newPerson.Projects.Contains(pi.Id)).ToList() : null
        };
            _context.Add(person);
            return Save();
        }

        public bool UpdatePerson(PersonDto personInfo)
        {
            var person = _context.Person.Include(pe => pe.ProjectItems).Include(pe => pe.Projects).
                FirstOrDefault(pe => pe.Id == personInfo.Id);
            person.Name = personInfo.Name;
            person.Username = personInfo.Username;
            person.PhoneNumber = personInfo.PhoneNumber;
            person.Email = personInfo.Email;
            person.ProjectItems = personInfo.ProjectItems != null ? _context.ProjectItem.Where(pi => personInfo.ProjectItems.Contains(pi.Id)).ToList() : null;
            person.Projects = personInfo.Projects != null ? person.Projects = _context.Project.Where(pi => personInfo.Projects.Contains(pi.Id)).ToList() : null;

            _context.Update(person);
            return Save();
        }

        public bool DeletePerson(PersonDto personDto)
        {
            var person = _context.Person.FirstOrDefault(p => p.Id == personDto.Id);
            _context.Remove(person);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}


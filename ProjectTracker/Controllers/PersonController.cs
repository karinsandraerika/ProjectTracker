﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Data;
using ProjectTracker.Interfaces;
using ProjectTracker.Models;
using ProjectTracker.Repository;
using ProjectTracker.Dto;


namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpGet]
        //[Route("getallpersons")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetPersons()
        {
            var persons = _mapper.Map<List<PersonDto>>(_personRepository.GetPersons());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetPerson(int id)
        {
            if (!_personRepository.PersonExists(id))
            {
                return NotFound();
            }

            var person = _mapper.Map<PersonDto>(_personRepository.GetPerson(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult PostPerson([FromBody] PersonDto newPerson)
        {
            if (newPerson == null)
            {
                return BadRequest(ModelState);
            }

            var person = _personRepository.GetPersons().Where(pe => pe.Username == newPerson.Username).FirstOrDefault();

            if (person != null)
            {
                ModelState.AddModelError("", "Username already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personMap = _mapper.Map<Person>(newPerson);

            if (!_personRepository.CreatePerson(personMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Sucessfully added");
        }

        [HttpPut("{personId}")]
        public IActionResult UpdatePerson(int personId, [FromBody] PersonDto personInfo)
        {
            if (personInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (personId != personInfo.Id)
            {
                return BadRequest(ModelState);  
            }

            if (!_personRepository.PersonExists(personId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var personMap = _mapper.Map<Person>(personInfo);

            if (!_personRepository.UpdatePerson(personMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            //return NoContent();
            return Ok("Succesfully updated");
        }


        /*
        /

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}


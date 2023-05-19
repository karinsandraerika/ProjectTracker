
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;
using ProjectTracker.Repository;
using ProjectTracker.Data;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Interfaces;
using ProjectTracker.Dto;


namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository,IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPersons()
        {
            var persons = _personRepository.GetPersons();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            var person = _personRepository.GetPerson(id);

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

            if (!_personRepository.CreatePerson(newPerson))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully added" });
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
                return BadRequest(ModelState);
            }
            
            if (!_personRepository.UpdatePerson(personInfo))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully updated" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            if (!_personRepository.PersonExists(id))
            {
                return NotFound();
            }

            var personDelete = _personRepository.GetPerson(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_personRepository.DeletePerson(personDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return Ok(new { message = "Successfully deleted" });
        }

    }
}


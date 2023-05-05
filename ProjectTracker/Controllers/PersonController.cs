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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly DatabaseContext _DatabaseContext;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository, DatabaseContext databaseContext,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _DatabaseContext = databaseContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetPersons()
        {
            var persons = _mapper.Map<List<PersonDto>>(_personRepository.GetPersons());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(persons);
        }


        /*
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

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

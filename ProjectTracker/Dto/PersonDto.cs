using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ProjectTracker.Models;

namespace ProjectTracker.Dto
{
	public class PersonDto
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        //public List<ProjectItem>? ProjectItems { get; set; }
    }
}


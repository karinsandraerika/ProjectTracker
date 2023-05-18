using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ProjectTracker.Models;

namespace ProjectTracker.Dto
{
	public class PersonDto
	{
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public List<int>? ProjectItems { get; set; }
        public List<int>? Projects { get; set; }
    }
}


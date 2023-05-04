using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Models
{
	public class Person
	{
        [Key] // primary key
        public int Id { get; set; }
      
        public string? Name { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public List<ProjectItem>? ProjectItems { get; set; }
    }
}


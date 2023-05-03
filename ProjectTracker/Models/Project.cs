using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Models
{
	public class Project
	{
        [Key] // primary key
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProjectItem> Tasks { get; set; }
        public List<Person> Persons { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Models
{
	public class ProjectItem
	{
        [Key] // primary key
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Person>? PersonList { get; set; }
        public string? Importance { get; set; }
        public string? Completed { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public int TimeToComplete { get; set; }
	}
}


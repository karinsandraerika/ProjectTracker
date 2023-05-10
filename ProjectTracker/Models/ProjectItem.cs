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
        public List<Person>? Persons { get; set; }
        public string? Importance { get; set; }
        public string? Completed { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Time to complete")]
        public TimeSpan TimeToComplete { get; set; }
        public Project? Project { get; set; }
    }
}


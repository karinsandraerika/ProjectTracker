using System;
using System.ComponentModel.DataAnnotations;
using ProjectTracker.Enums;

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
        public Importance? Importance { get; set; }
        public CompletionStatus? Completed { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Hours to complete")]
        public int HoursToComplete { get; set; }
        public Project? Project { get; set; }
    }
}


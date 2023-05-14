using System;
using ProjectTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ProjectTracker.Enums;

namespace ProjectTracker.Dto
{
	public class ProjectItemDto
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Importance? Importance { get; set; } = null;
        public CompletionStatus? Completed { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        //public TimeSpan TimeToComplete { get; set; }
    }
}


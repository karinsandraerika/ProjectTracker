using System;
using ProjectTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjectTracker.Dto
{
	public class ProjectItemDto
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Importance { get; set; }
        public string? Completed { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public int TimeToComplete { get; set; }
    }
}


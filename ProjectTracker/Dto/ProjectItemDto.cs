﻿using System;
using ProjectTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ProjectTracker.Enums;

namespace ProjectTracker.Dto
{
	public class ProjectItemDto
	{
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Importance? Importance { get; set; } 
        public CompletionStatus? Completed { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        public List<int>? Persons { get; set; }
        public int? ProjectId { get; set; }
        public int? HoursToComplete { get; set; }
    }
}


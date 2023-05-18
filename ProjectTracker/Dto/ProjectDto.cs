using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Dto
{
	public class ProjectDto
	{
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<int>? Persons { get; set; }
        public List<int>? ProjectItems { get; set; }
    }
}


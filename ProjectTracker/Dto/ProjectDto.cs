using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracker.Dto
{
	public class ProjectDto
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClientProjectPlanner
{
	public class ProjectItem
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        //public Importance? Importance { get; set; } = null;
        //public CompletionStatus? Completed { get; set; }
        public string? Importance { get; set; }
        public string? Completed { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int>? Persons { get; set; }
        public int ProjectId { get; set; }

        public void PrintItem()
        {
            Console.WriteLine("Id: " + Id + ", Name: " + Name + ", Description: " + Description);
        }
    }

}


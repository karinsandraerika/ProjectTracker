using System;
namespace ClientProjectPlanner
{
	public class Project
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<int>? Persons { get; set; }
        public List<int>? ProjectItems { get; set; }

        public void PrintProject()
        {
            //What happens when Lists are empty or null? Should I add default empty list?
            Console.WriteLine($"Id: {Id} Name: {Name} Description: {Description} Persons: " +
                $"{string.Join(", ", Persons)} Project items: {string.Join(", ", ProjectItems)}");
                
        }
    }
}


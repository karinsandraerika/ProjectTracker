using System;
using ProjectTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectTracker.Data
{
	public class DatabaseContext : DbContext
	{
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; } = default!; //Check default!
        public DbSet<Project> Project { get; set; } = default!;
        public DbSet<ProjectItem> ProjectItem { get; set; } = default!;
    }
}


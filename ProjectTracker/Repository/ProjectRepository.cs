using System;
using ProjectTracker.Data;
using ProjectTracker.Interfaces;
using ProjectTracker.Models;

namespace ProjectTracker.Repository
{
	public class ProjectRepository : IProjectRepository
	{
        private readonly DatabaseContext _context;

        public ProjectRepository(DatabaseContext context)
		{
            _context = context;
        }

        public ICollection<Project> GetProjects()
        {
            return _context.Project.ToList();
        }
    }
}


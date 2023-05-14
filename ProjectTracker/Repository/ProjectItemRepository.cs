    using System;
using ProjectTracker.Data;
using ProjectTracker.Interfaces;
using ProjectTracker.Models;

namespace ProjectTracker.Repository
{
	public class ProjectItemRepository : IProjectItemRepository
	{
        private readonly DatabaseContext _context;

        public ProjectItemRepository(DatabaseContext context)
		{
            _context = context;
		}

        public ProjectItem GetProjectItem(int id)
        {
            return _context.ProjectItem.Where(pi => pi.Id == id).FirstOrDefault();
        }

        public ICollection<ProjectItem> GetProjectItems()
        {
            return _context.ProjectItem.ToList();
        }

        public bool ProjectItemExists(int id)
        {
            return _context.ProjectItem.Any(p => p.Id == id);
        }

        public bool CreateProjectItem(ProjectItem projectItem)
        {
            //var ProjectItemPersonEntity = _context.Person.Where(p => p.Id == personId).FirstOrDefault();
            //Add the relationship for project when we've added a many-to-many.s
            _context.Add(projectItem);
            return Save();
        }

        public bool UpdateProjectItem(ProjectItem projectItem)
        {
            _context.Update(projectItem);
            return Save();
        }

        public bool DeleteProjectItem(ProjectItem projectItem)
        {
            _context.Remove(projectItem);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}


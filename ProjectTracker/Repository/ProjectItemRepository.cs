﻿    using System;
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
    }
}


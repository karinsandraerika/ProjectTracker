﻿using System;
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

        public Project GetProject(int id)
        {
            return _context.Project.Where(pr => pr.Id == id).FirstOrDefault();
        }

        public ICollection<Project> GetProjects()
        {
            return _context.Project.ToList();
        }

        public bool ProjectExists(int id)
        {
            return _context.Project.Any(p => p.Id == id);
        }

        public bool CreateProject(Project project)
        {
            _context.Add(project);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}


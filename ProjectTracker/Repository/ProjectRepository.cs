using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Dto;
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

        public ProjectDto GetProject(int id)
        {
            var project = _context.Project.Include(p => p.ProjectItems).Include(p => p.Persons).FirstOrDefault(p => p.Id == id);
            var projectDto = new ProjectDto()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Persons = project.Persons != null ? project.Persons.Select(p => p.Id).ToList() : null,
                ProjectItems = project.ProjectItems != null ? project.ProjectItems.Select(p => p.Id).ToList() : null
            };
            return projectDto;
        }

        public ICollection<ProjectDto> GetProjects()
        {
            var projects = _context.Project.Include(p => p.ProjectItems).Include(p => p.Persons).ToList();
            var projectDtos = new List<ProjectDto>();
            foreach (var project in projects)
            {
                var projectDto = new ProjectDto()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    Persons = project.Persons != null ? project.Persons.Select(p => p.Id).ToList() : null,
                    ProjectItems = project.ProjectItems != null ? project.ProjectItems.Select(p => p.Id).ToList() : null
                };
                projectDtos.Add(projectDto);
            }
            return projectDtos;
        }

        public bool ProjectExists(int id)
        {
            return _context.Project.Any(p => p.Id == id);
        }

        public bool CreateProject(ProjectDto newProject)
        {
            var project = new Project()
            {
                Id = newProject.Id,
                Name = newProject.Name,
                Description = newProject.Description,
                Persons = newProject.Persons != null ? _context.Person.Where(p => newProject.Persons.Contains(p.Id)).ToList() : null,
                ProjectItems = newProject.ProjectItems != null ? _context.ProjectItem.Where(p => newProject.ProjectItems.Contains(p.Id)).ToList() : null
            };
            _context.Add(project);
            return Save();
        }

        public bool UpdateProject(ProjectDto projectInfo)
        {
            var project = _context.Project.Include(p => p.ProjectItems).Include(p => p.Persons).
                FirstOrDefault(p => p.Id == projectInfo.Id);
            //Check that the name does not exist for another project
            project.Name = projectInfo.Name ?? project.Name;
            project.Description = projectInfo.Description ?? project.Description;
            if (projectInfo.Persons != null)
            {
                project.Persons = _context.Person.Where(p => projectInfo.Persons.Contains(p.Id)).ToList();
            }
            if (projectInfo.ProjectItems != null)
            {
                project.ProjectItems = _context.ProjectItem.Where(p => projectInfo.ProjectItems.Contains(p.Id)).ToList();
            }
            _context.Update(project);
            return Save();
        }

        public bool DeleteProject(ProjectDto projectDto)
        {
            var project = _context.Project.FirstOrDefault(p => p.Id == projectDto.Id);
            _context.Remove(project);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}


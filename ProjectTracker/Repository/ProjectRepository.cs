using System;
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
            Project? project = _context.Project.Include(p => p.ProjectItems).Include(p => p.Persons).FirstOrDefault(p => p.Id == id);
            ProjectDto projectDto = new ProjectDto()
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
            List<ProjectDto> projectDtos = new List<ProjectDto>();
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
            project.ProjectItems = projectInfo.ProjectItems != null ? _context.ProjectItem.Where(p => projectInfo.ProjectItems.Contains(p.Id)).ToList() : null;
            project.Persons = projectInfo.Persons != null ? _context.Person.Where(p => projectInfo.Persons.Contains(p.Id)).ToList() : null;
            /*var person = _context.Person.Include(pe => pe.ProjectItems).Include(pe => pe.Projects).
                FirstOrDefault(pe => pe.Id == personInfo.Id);
            person.ProjectItems = personInfo.ProjectItems != null ? _context.ProjectItem.Where(pi => personInfo.ProjectItems.Contains(pi.Id)).ToList() : null;
            person.Projects = personInfo.Projects != null ? _context.Project.Where(pi => personInfo.Projects.Contains(pi.Id)).ToList() : null;*/
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


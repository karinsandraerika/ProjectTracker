    using System;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using ProjectTracker.Data;
using ProjectTracker.Dto;
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

        public ProjectItemDto GetProjectItem(int id)
        {
            ProjectItem? item = _context.ProjectItem.Include(pi => pi.Persons).Include(p => p.Project).FirstOrDefault(pi => pi.Id == id);
            ProjectItemDto itemDto = new ProjectItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Importance = item.Importance,
                Completed = item.Completed,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                HoursToComplete = item.HoursToComplete,
                Persons = item.Persons != null ? item.Persons.Select(p => p.Id).ToList() : null,
                ProjectId = item.Project != null ? item.Project.Id : null
            };
            return itemDto;
        }

        public ICollection<ProjectItemDto> GetProjectItems()
        {
            var projectItems = _context.ProjectItem.Include(p => p.Persons).Include(p => p.Project).ToList();
            List<ProjectItemDto> projectItemsDtos = new List<ProjectItemDto>();
            foreach (var item in projectItems)
            {
                var itemDto = new ProjectItemDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Importance = item.Importance,
                    Completed = item.Completed,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    HoursToComplete = item.HoursToComplete,
                    Persons = item.Persons != null ? item.Persons.Select(p => p.Id).ToList() : null
                };
                
                if (item.Project != null)
                {
                    int projectId = item.Project.Id;
                    itemDto.ProjectId = projectId;
                }
                projectItemsDtos.Add(itemDto);
            }
            return projectItemsDtos;
        }

        public bool ProjectItemExists(int id)
        {
            return _context.ProjectItem.Any(p => p.Id == id);
        }

        public bool CreateProjectItem(ProjectItemDto newItem)
        {
            var item = new ProjectItem()
            {
                Id = newItem.Id,
                Name = newItem.Name,
                //TODO Check enums. Maybe add validation here.
                Description = newItem.Description,
                Importance = newItem.Importance,
                Completed = newItem.Completed,
                StartDate = newItem.StartDate,
                EndDate = newItem.EndDate,
                HoursToComplete = newItem.HoursToComplete,
                Persons = newItem.Persons != null ? _context.Person.Where(p => newItem.Persons.Contains(p.Id)).ToList() : null,
                Project = newItem.ProjectId != null ? _context.Project.FirstOrDefault(p => p.Id == newItem.ProjectId) : null
            };
            
            _context.Add(item);
            return Save();
        }

        public bool UpdateProjectItem(ProjectItemDto projectItemInfo)
        {
            var item = _context.ProjectItem.Include(p => p.Persons).
                FirstOrDefault(p => p.Id == projectItemInfo.Id);
            if (item == null)
            {
                return false;
            }
            if (projectItemInfo.Persons != null)
            {
                var persons = _context.Person.Where(p => projectItemInfo.Persons.Contains(p.Id)).ToList();
                item.Persons = persons;
            }

            _context.Update(item);
            return Save();
        }

        public bool DeleteProjectItem(ProjectItemDto projectItemDto)
        {
            var item = _context.ProjectItem.FirstOrDefault(p => p.Id == projectItemDto.Id);
            _context.Remove(item);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}


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

        public ICollection<ProjectItem> GetProjectItems()
        {
            return _context.ProjectItem.ToList();
        }

        /*
        public bool CreateProjectItem(int ProjectId, int PersonId, ProjectItem item)
        {
            var projectItemProject = _context.Project.Where(a => a.Id == ProjectId).FirstOrDefault();
            var projectItemPerson = _context.Person.Where(a => a.Id == PersonId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);

            return Save();
        }

        public bool DeleteProjectItem(ProjectItem item)
        {
            throw new NotImplementedException();
        }

        public ProjectItem GetProjectItem(int id)
        {
            throw new NotImplementedException();
        }

        public ProjectItem GetProjectItem(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProjectItem> GetProjectItems()
        {
            throw new NotImplementedException();
        }

        public bool ProjectItemExists(int itemId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateProjectItem(int ProjectId, int PersonId, ProjectItem item)
        {
            throw new NotImplementedException();
        }
        */
    }
}


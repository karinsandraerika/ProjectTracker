using System;
using ProjectTracker.Models;

namespace ProjectTracker.Interfaces
{
	public interface IProjectItemRepository
	{
        ICollection<ProjectItem> GetProjectItems();

        /*
        ProjectItem GetProjectItem(int id);
        ProjectItem GetProjectItem(string name);

        //ProjectItem GetPokemonTrimToUpper(PokemonDto pokemonCreate);
        //decimal GetPokemonRating(int pokeId);
        bool ProjectItemExists(int itemId);
        bool CreateProjectItem(int ProjectId, int PersonId, ProjectItem item);
        bool UpdateProjectItem(int ProjectId, int PersonId, ProjectItem item);
        bool DeleteProjectItem(ProjectItem item);
        bool Save();
        */
    }
}


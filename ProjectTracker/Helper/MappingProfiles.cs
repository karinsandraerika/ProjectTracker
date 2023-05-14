using System.Diagnostics.Metrics;
using AutoMapper;
using ProjectTracker.Dto;
using ProjectTracker.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectItem, ProjectItemDto>();

            CreateMap<PersonDto, Person>();
            CreateMap<ProjectDto, Project>();
            CreateMap<ProjectItemDto, ProjectItem>();

        }
    }
}


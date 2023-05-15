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
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<ProjectItem, ProjectItemDto>().ReverseMap();
        }
    }
}


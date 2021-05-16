using AutoMapper;
using Exchange.Dtos;
using Server.Database.Entities;

namespace Server.Mapping.Profiles
{
    public class ProjectMapping : Profile
    {
        public ProjectMapping()
        {
            CreateMap<Project, ProjectOutDto>();
            CreateMap<ProjectInDto, Project>();
        }
    }
}

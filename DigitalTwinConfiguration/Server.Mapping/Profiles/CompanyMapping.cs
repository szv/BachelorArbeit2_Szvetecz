using AutoMapper;
using Exchange.Dtos;
using Server.Database.Entities;

namespace Server.Mapping.Profiles
{
    public class CompanyMapping : Profile
    {
        public CompanyMapping()
        {
            CreateMap<Company, CompanyOutDto>();
            CreateMap<CompanyInDto, Company>();
        }
    }
}

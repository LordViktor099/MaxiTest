using AutoMapper;
using TestMaxiT.Models.Dtos;
using TestMaxiT.Models.Entities;

namespace TestMaxiT.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<BeneficiaryDto, Beneficiary>().ReverseMap();
        }

    }
}

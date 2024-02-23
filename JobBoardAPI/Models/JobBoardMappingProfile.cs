using AutoMapper;
using JobBoardAPI.Entities;

namespace JobBoardAPI.Models
{
    public class JobBoardMappingProfile : Profile
    {
        public JobBoardMappingProfile()
        {
            CreateMap<JobAdvertisement, JobAdvertisementDto>()
                .ForMember(m => m.CategoryName, c => c.MapFrom(s => s.Category.Name))
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<CreateJobAdvertisementDto, JobAdvertisement>()
                .ForMember(m => m.Address, c => c.MapFrom(dto => new Address() { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));
        }
    }
}

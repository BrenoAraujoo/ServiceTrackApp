using AutoMapper;
using ServiceTrackHub.Application.DTOS;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<User, UserDTORequest>().ReverseMap();
            CreateMap<User, UserDTOResponse>();

            /*
            CreateMap<UserDTORequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            */


            CreateMap<Service,ServiceDTO>().ReverseMap();
        }
    }
}

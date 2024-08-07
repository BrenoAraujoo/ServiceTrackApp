using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.ViewModel.Task;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<User, CreateUserInputModel>().ReverseMap();
            CreateMap<User, UserViewModel>();

            /*
            CreateMap<UserDTORequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            */


            CreateMap<Tasks, TasksInputViewModel>().ReverseMap();
            CreateMap<Tasks, TasksViewModel>();

        }
    }
}

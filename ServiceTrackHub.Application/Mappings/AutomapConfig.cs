using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.ViewModel.Task;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Application.Mappings
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            CreateMap<UpdateUserInputModel, User>()
                    .ForAllMembers(opt => opt.Condition((
                        src, dest, srcMember) =>
                        !string.IsNullOrEmpty(srcMember?.ToString()))
                        );


            CreateMap<User, CreateUserInputModel>().ReverseMap();
            //CreateMap<User, UpdateUserInputModel>().ReverseMap();
            CreateMap<User, UserViewModel>();

            /*
            CreateMap<UserDTORequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            */


            CreateMap<Tasks, TasksInputModel>().ReverseMap();
            CreateMap<Tasks, TasksViewModel>();

        }
    }
}

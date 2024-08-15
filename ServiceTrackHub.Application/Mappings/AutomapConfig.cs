using AutoMapper;
using ServiceTrackHub.Application.InputViewModel.Task;
using ServiceTrackHub.Application.InputViewModel.TaskType;
using ServiceTrackHub.Application.InputViewModel.User;
using ServiceTrackHub.Application.ViewModel.Task;
using ServiceTrackHub.Application.ViewModel.TaskType;
using ServiceTrackHub.Application.ViewModel.User;
using ServiceTrackHub.Domain.Entities;

namespace ServiceTrackHub.Application.Mappings
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            //Maping without consider null or empty properties.
            CreateMap<UpdateUserInputModel, User>()
                .ForAllMembers
                (opt => opt.Condition((src, dest, srcMember) =>
                !string.IsNullOrEmpty(srcMember?.ToString())));


            CreateMap<User, CreateUserInputModel>().ReverseMap();
            CreateMap<User, UserViewModel>();

            CreateMap<Tasks, TasksInputModel>().ReverseMap();
            CreateMap<Tasks, TasksViewModel>();

            CreateMap<TaskType, TasksInputModel>();

            CreateMap<TaskType, TaskTypeViewModel>();
            CreateMap<TaskType, CreateTaskTypeInputModel>().ReverseMap();
            
        }
    }
}

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
            //Mapping without consider null or empty properties.
            CreateMap<UpdateUserModel, User>()
                .ForAllMembers
                (opt => opt.Condition((src, dest, srcMember) =>
                !string.IsNullOrEmpty(srcMember?.ToString())));
            
            CreateMap<UpdateTaskTypeModel, TaskType>()
                .ForAllMembers(opt  => opt.Condition((src,dest, srcMember) =>
                    !string.IsNullOrEmpty(srcMember?.ToString())));
            
            CreateMap<UpdateTaskModel, Tasks>()
                .ForAllMembers(opt  => opt.Condition((src,dest, srcMember) =>
                    !string.IsNullOrEmpty(srcMember?.ToString())));

            CreateMap<User, CreateUserModel>().ReverseMap();
            CreateMap<User, UserViewModel>();

            CreateMap<Tasks, CreateTaskModel>().ReverseMap();
            CreateMap<Tasks, TasksViewModel>();

            CreateMap<TaskType, TaskTypeViewModel>().ReverseMap();
            CreateMap<TaskType, CreateTaskTypeModel>().ReverseMap();
            
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.InputViewModel.Task
{
    public record TasksInputModel (
       [Required(ErrorMessage = "Task name is required"), 
        MinLength (3,ErrorMessage = "Min is 3"), 
        MaxLength (100,ErrorMessage = "Max is 100")]
        string Name,
        [Required(ErrorMessage = "Task Description is required"),
        MinLength (3,ErrorMessage = "Min is 3"),
        MaxLength (100,ErrorMessage = "Max is 100")]
        string Description,
        [Required(ErrorMessage = "Task UserID is required")]
        Guid? UserId);
}

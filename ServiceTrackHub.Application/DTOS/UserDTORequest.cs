using System.ComponentModel.DataAnnotations;

namespace ServiceTrackHub.Application.DTOS
{
    public class UserDTORequest
    {
        [Required(ErrorMessage ="The name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}

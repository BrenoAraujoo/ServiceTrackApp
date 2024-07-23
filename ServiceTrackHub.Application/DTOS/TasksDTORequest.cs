using ServiceTrackHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTrackHub.Application.DTOS
{
    public class TasksDTORequest
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage ="User Id is required")]
        public int UserId { get; set; }
    }
}

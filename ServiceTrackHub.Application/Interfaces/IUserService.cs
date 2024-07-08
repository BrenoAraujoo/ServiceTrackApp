using ServiceTrackHub.Application.DTOS;

namespace ServiceTrackHub.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTOResponse>> GetUsers();
        Task<UserDTOResponse> GetById(int? id);
        Task<UserDTOResponse> Create(UserDTORequest UserDTO);
        Task<UserDTOResponse> Update(int? id, UserDTORequest UserDTO);
        Task Remove(int? id);
    }
}

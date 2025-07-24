using GoDate.API.Entities.Domain;
using GoDate.API.Entities.DTO;

namespace GoDate.API.Repositories.UserRepo
{
    public interface IUserRepository
    {
        void Update(User user);                 // Don't want to be async as not returning anything
        Task<bool> SaveAllAsync();
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByNameAsync(string username);
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserDto?> GetUserAsync(string username);
    }
}

using GoDate.API.Entities.Domain;

namespace GoDate.API.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}

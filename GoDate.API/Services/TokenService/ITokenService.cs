using GoDate.API.Entities.Domain;

namespace GoDate.API.Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}

using Core.Entities;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user, string existingToken = null);
    }
}

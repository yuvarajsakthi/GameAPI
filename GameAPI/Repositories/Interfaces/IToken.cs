using GameAPI.Models;

namespace GameAPI.Repositories.Interfaces
{
    public interface IToken
    {
        string GenerateToken(User user, IEnumerable<string> roles);
        bool ValidateToken(string token);
    }
}

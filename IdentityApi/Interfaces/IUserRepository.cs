using Common.Models;

namespace IdentityApi.Interfaces;

public interface IUserRepository
{
    bool GetUserExistsByEmail(string email);
    bool AddUser(User user);
    User? GetUserByEmail(string email);
}
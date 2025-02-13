using Common.Models;
using IdentityApi.Data;
using IdentityApi.Interfaces;

namespace IdentityApi.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    public bool GetUserExistsByEmail(string email)
    {
        return context.Users.Any(u => u.Email == email);
    }

    public User? GetUserByEmail(string email)
    {
        return context.Users.FirstOrDefault(u => u.Email == email);
    }

    public bool AddUser(User user)
    {
        context.Users.Add(user);
        
        return Save();
    }
    
    private bool Save() => context.SaveChanges() > 0;
}
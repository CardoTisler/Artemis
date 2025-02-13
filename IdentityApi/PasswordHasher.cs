namespace IdentityApi;

public class PasswordHasher : PasswordHasher.IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {   
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }

    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
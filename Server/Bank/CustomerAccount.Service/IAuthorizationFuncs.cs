namespace CustomerAccount.Service;
public interface IAuthorizationFuncs
{
    public string GenerateSalt(int nSalt);
    public string HashPassword(string password, string salt, int nIterations, int nHash);
}
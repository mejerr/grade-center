namespace GradeCenter.Server.Services
{
    public interface IIdentityService
    {
        string GenerateToken(string secret, string userId, string userName);
    }
}

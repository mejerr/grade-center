namespace GradeCenter.Server.Services
{
    public interface IIdentityService
    {
        public string GenerateToken(string secret, string userId);
    }
}

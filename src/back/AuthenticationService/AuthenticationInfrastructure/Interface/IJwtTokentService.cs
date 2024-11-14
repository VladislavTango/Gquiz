
namespace AuthenticationInfrastructure.Interface
{
    public interface IJwtTokentService
    {
        public string GenerateToken(Guid userId, string userName, string Role);
    }
}

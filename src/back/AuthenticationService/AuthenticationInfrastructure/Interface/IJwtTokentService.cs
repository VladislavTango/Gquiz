
namespace AuthenticationInfrastructure.Interface
{
    public interface IJwtTokentService
    {
        public string GenerateToken(Guid Id,string userName, string Role);
    }
}

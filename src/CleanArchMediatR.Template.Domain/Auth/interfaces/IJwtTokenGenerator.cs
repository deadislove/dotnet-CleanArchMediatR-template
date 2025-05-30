using CleanArchMediatR.Template.Domain.Entities;

namespace CleanArchMediatR.Template.Domain.Auth.interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
        public string GenerateRefreshToken(User user);
    }
}

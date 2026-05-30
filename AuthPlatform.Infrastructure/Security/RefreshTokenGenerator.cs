
using AuthPlatform.Application.Auth.Security;
using System.Security.Cryptography;

namespace AuthPlatform.Infrastructure.Security
{


    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }
    }
}

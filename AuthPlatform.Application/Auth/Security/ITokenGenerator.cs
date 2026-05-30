using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Auth.Security
{
    public interface ITokenGenerator
    {
        string GenerateAccessToken(
            int authUserId,
            string userName,
            List<string> permissions);
    }
}

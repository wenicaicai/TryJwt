using JWT.Server.Models;
using Trivial.Security;

namespace JWT.Server.Services.Interfaces
{
    public interface IJwtService
    {
        string Encode(JwtPayload payload);

        string EncodeConst(JwtPayload payload);

        JsonWebToken<JwtPayload> Decode(string token);
    }
}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Server.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Trivial.Security;

namespace JWT.Server.Services
{
    public class JwtService : IJwtService
    {
        public string Encode(Models.JwtPayload payload)
        {
            var whatIsThis = Signature();
            var jwt = new JsonWebToken<Models.JwtPayload>(payload, Signature());
            return jwt.ToEncodedString();
        }

        /// <summary>
        /// ref https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public string EncodeConst(Models.JwtPayload payload)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Name,payload.aud)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Const.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, payload.aud)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = payload.iss,
                Audience = payload.aud,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public JsonWebToken<Models.JwtPayload> Decode(string token)
        {
            return JsonWebToken<Models.JwtPayload>.Parse(token, Signature());
        }

        private HashSignatureProvider Signature()
        {
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET");
            return HashSignatureProvider.CreateHS256(secret ?? "secret");
        }
    }
}

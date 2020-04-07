using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public LoginController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet]
        [Route("Login")]
        public ActionResult<string> Login(string userId, string pwd)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(pwd))
            {
                //var claims = new[]
                //{
                //    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                //    new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                //    new Claim(ClaimTypes.Name,userId)
                //};
                Models.JwtPayload jwtPayload = new Models.JwtPayload
                {
                    aud = "Yang",
                    claimA = "to be world value",
                    exp = $"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}",
                    nbf = $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}",
                    iat = $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}",
                    iss = "Yang",
                    sub = "login"
                };
                return StatusCode(StatusCodes.Status201Created, new { token = _jwtService.EncodeConst(jwtPayload) });
            }
            return StatusCode(StatusCodes.Status400BadRequest, "userId or pwd is invalid.");
        }
    }
}
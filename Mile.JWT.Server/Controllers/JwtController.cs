using System;
using System.Collections.Generic;
using JWT.Server.Models;
using JWT.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class JwtController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public JwtController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        public ActionResult<string> Encode([FromBody]JwtPayload jwtPayload)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _jwtService.Encode(jwtPayload));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, e.Message);
            }
        }

        [HttpGet]
        public ActionResult<string> Decode(string token)
        {
            try
            {
                return Ok(_jwtService.Decode(token));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status201Created, e.Message);
            }
        }
    }
}
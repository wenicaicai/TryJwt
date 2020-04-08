using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsParallelLinQController : ControllerBase
    {
        private readonly IAsParallelLinQService _asParallelLinQService;
        public AsParallelLinQController(IAsParallelLinQService asParallelLinQService)
        {
            _asParallelLinQService = asParallelLinQService;
        }

        [HttpGet]
        public ActionResult AsParallel()
        {
            return StatusCode(StatusCodes.Status200OK, _asParallelLinQService.Print());
        }

        [HttpGet]
        [Route("ThreadNosafe")]
        public ActionResult ThreadNosafe()
        { 
            return StatusCode(StatusCodes.Status200OK, _asParallelLinQService.ThreadUnsafe());

        }

        [HttpGet]
        [Route("ThreadSafe")]
        public ActionResult ThreadSafe()
        {
            return StatusCode(StatusCodes.Status200OK, _asParallelLinQService.ThreadSafe());
        }

        [HttpGet]
        [Route("ThreadDefault")]
        public ActionResult ThreadDefault()
        {
            return StatusCode(StatusCodes.Status200OK, _asParallelLinQService.ThreadDefault());
        }
    }
}
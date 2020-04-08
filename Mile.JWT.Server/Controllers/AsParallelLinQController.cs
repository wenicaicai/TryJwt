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
        private readonly IAggregateLinQService _aggregateLinQService;

        public AsParallelLinQController(IAsParallelLinQService asParallelLinQService, IAggregateLinQService aggregateLinQService)
        {
            _asParallelLinQService = asParallelLinQService;
            _aggregateLinQService = aggregateLinQService;
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

        [HttpGet]
        [Route("Aggregate")]
        public ActionResult Aggregate()
        {
            return StatusCode(StatusCodes.Status200OK, _aggregateLinQService.AggregateSum());
        }
    }
}
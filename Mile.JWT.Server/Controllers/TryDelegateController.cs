using JWT.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JWT.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TryDelegateController : ControllerBase
    {
        private readonly ITryDelegateService _tryDelegateService;
        public TryDelegateController(ITryDelegateService tryDelegateService)
        {
            _tryDelegateService = tryDelegateService;
        }

        [HttpGet]
        public ActionResult UseDelegate(string name)
        {
            var result = _tryDelegateService.RunDelegate(name);
            return StatusCode(StatusCodes.Status200OK,result);
        }

        [HttpGet]
        [Route("UseCompare")]
        public ActionResult UseCompare()
        {
            var result = _tryDelegateService.AllSameCompanyPet();
            return StatusCode(StatusCodes.Status200OK,result);
        }

        [HttpGet]
        [Route("UseThread")]
        public async Task<string> UseThread()
        {
            var result = await _tryDelegateService.RunThread();
            return result;
        }
    }
}
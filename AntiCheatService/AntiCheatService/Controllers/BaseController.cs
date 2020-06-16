using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntiCheatService.Controllers
{
    [Route("/")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok(); //200
        }
    }
}

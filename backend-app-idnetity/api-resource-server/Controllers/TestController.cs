using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_resource_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult GetPublic()
        {
            var result = new Result("Se llamó al servicio publico de manera satisfactoria.!");
            return Ok(result);
        }
        [Authorize]
        [HttpGet("private")]
        public IActionResult GetPrivate()
        {
            var result = new Result("Se llamó al servicio privado de manera satisfactoria.!");
            return Ok(result);
        }

        [Authorize("read:permissions")]
        [HttpGet("permission")]
        public IActionResult GetPermission()
        {
            var result = new Result("Se llamó al servicio privado con permisos de manera satisfactoria.!");
            return Ok(result);
        }
    }

    public class Result
    {
        public Result(string msg)
        {
            this.Msg = msg;
        }
        public string Msg { get; set; }
    }
}

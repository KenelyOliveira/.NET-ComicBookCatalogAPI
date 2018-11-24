using Bibliotech.Facade;
using Bibliotech.Models;

namespace Bibliotech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(
            //[FromBody]: o parametro será informado no body da requisisão
            [FromBody]Usuario usuario,
            //[FromServices]: o parãmetro é registrado como um serviço no startup e injetados aqui.
            //  esse serviços podem ser registrados como Singleton, Transient, etc
            //  está aqui pra eu saber que isso existe, porque eu costumo chamar uma facade via factory e dentro dessa factory fazer
            //  a injeção de dependencia necessária
            [FromServices]LoginFacade loginFacade)
        {

            var retorno = loginFacade.Login(usuario);

            if (retorno.IsAutenticado)
                return Ok(retorno);
            else return BadRequest(retorno);
        }
    }
}

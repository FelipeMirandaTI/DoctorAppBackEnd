using API.Errores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components;
namespace API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("errores/{codigo}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController:BaseApiController
    {
        public IActionResult Error(int codigo)
        {
            return new ObjectResult(new ApiErrorResponse(codigo));
        }

    }
}

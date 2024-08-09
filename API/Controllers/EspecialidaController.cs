using BLL.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace API.Controllers
{
    [Authorize(Roles ="Admin,Agendador")]
    public class EspecialidaController:BaseApiController
    {
        private readonly IEspecialidadServicio _especialidadServicio;
        private ApiResponse _response;

        public EspecialidaController(IEspecialidadServicio especialidadServicio)
        {
            _especialidadServicio = especialidadServicio;
            _response = new ();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _especialidadServicio.ObtenerTodos();
                _response.IsExitoso = true; 
                _response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception e) 
            {
                _response.IsExitoso = false;
                _response.Message = e.Message;
                _response.StatusCode=System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);
            
        }
        [HttpPost]
        public async Task<IActionResult> Crear(EspecialidadDTO especialidadDTO)
        {
            try 
            {
                await _especialidadServicio.Agregar(especialidadDTO);
                _response.IsExitoso = true;
                _response.StatusCode= System.Net.HttpStatusCode.OK;
            }
            catch (Exception e) 
            {
                _response.IsExitoso = false;
                _response.Message = e.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }
        [HttpPut]
        public async Task<IActionResult> Editar(EspecialidadDTO especialidadDTO)
        {
            try 
            {
                await _especialidadServicio.Actualizar(especialidadDTO);    
                _response.IsExitoso = true;
                _response.StatusCode=System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception e) 
            {
                _response.IsExitoso = false;
                _response.Message = e.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }
        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            try 
            {
                await _especialidadServicio.Remover(id);
                _response.IsExitoso = true;
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;


            }
            catch (Exception e) 
            {
                _response.IsExitoso = false;
                _response.Message = e.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;

            }
            return Ok(_response);

        }
        [HttpGet("ListadoActivos")]
        public async Task<IActionResult> GetActivos()
        {
            try
            {
                _response.Resultado = await _especialidadServicio.ObtenerActivos();
                _response.IsExitoso = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                _response.IsExitoso = false;
                _response.Message = e.Message;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return Ok(_response);

        }


    }
}

using BLL.Servicios;
using BLL.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace API.Controllers
{
    public class MedicoController:BaseApiController
    {
        private readonly IMedicoServicio _medicoServicio;
        private ApiResponse _response;

        public MedicoController(IMedicoServicio medicoServicio)
        {
            _medicoServicio = medicoServicio;
            _response = new ();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _medicoServicio.ObtenerTodos();
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
        public async Task<IActionResult> Crear(MedicoDTO medicoDTO)
        {
            try 
            {
                await _medicoServicio.Agregar(medicoDTO);
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
        public async Task<IActionResult> Editar(MedicoDTO medicoDTO)
        {
            try 
            {
                await _medicoServicio.Actualizar(medicoDTO);    
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
                await _medicoServicio.Remover(id);
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


    }
}

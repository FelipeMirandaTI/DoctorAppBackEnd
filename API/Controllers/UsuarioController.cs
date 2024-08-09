using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
   //  [Authorize(Policy ="AdminAgendadorRol")]
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly ITokenServicio _tokenServicio;
        private ApiResponse _response;
        private readonly RoleManager<RolAplicacion> _roleManager;
        public UsuarioController(UserManager<UsuarioAplicacion> userManager,ITokenServicio tokenServicio, RoleManager<RolAplicacion> roleManager)
        {
            _userManager=userManager;
            _tokenServicio = tokenServicio;
            _response = new ();
            _roleManager = roleManager;
        }
        [Authorize(Policy = "AdminRol")]
        [HttpGet("GetUsuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioListaDTO>>> GetUsuarios()
        {
            var usuarios = await _userManager.Users.Select(u => new UsuarioListaDTO()
            {
                Username=u.UserName,
                Apellidos=u.Apellidos,
                Nombres=u.Nombres,
                Email=u.Email,
                Rol=string.Join(",",_userManager.GetRolesAsync(u).Result.ToArray())
            }).ToListAsync();
            _response.Resultado = usuarios;
            _response.IsExitoso = true;
            _response.StatusCode=System.Net.HttpStatusCode.OK;
            return Ok(_response);
        }
        [Authorize]
        [HttpGet("GetUsuarios/{id}")]
        public async Task<ActionResult <Usuario>> GetUsuario(int id) 
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync();
            return Ok(usuario);
        }
        [Authorize(Policy ="AdminRol")]
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDTO>> Registro(RegistroDTO registroDTO)
        { 
            if (await ExisteUsuario(registroDTO.Username)) return BadRequest("El usuario ya existe");
            var usuario = new UsuarioAplicacion
            {
                UserName = registroDTO.Username.ToLower(),
                Email = registroDTO.Email.ToLower(),
                Apellidos=registroDTO.Apellidos,
                Nombres=registroDTO.Nombres
            };

            var resultado = await _userManager.CreateAsync(usuario, registroDTO.Password);
            if (!resultado.Succeeded) return BadRequest(resultado.Errors);

            var rolResultado = await _userManager.AddToRoleAsync(usuario, registroDTO.Rol);
            if (!rolResultado.Succeeded) return BadRequest("Error al agregar el rol de usuario");
            {
                
            }
            return new UsuarioDTO 
            {
                Username = usuario.UserName,
                Token= await _tokenServicio.CrearToken(usuario)
            };
        }
        [HttpGet("ExisteUsuario")]
        private async Task<bool> ExisteUsuario(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioDTO>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (usuario == null) return Unauthorized("Usuario no valido");
            
            var resultado = await _userManager.CheckPasswordAsync(usuario,loginDto.Password);
            if (!resultado) return BadRequest("Password no valido");
            return new UsuarioDTO 
            {
                Username = usuario.UserName,
                Token= await _tokenServicio.CrearToken(usuario)
            };

        }
        [HttpGet("ListadoRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles.Select(x => new { NombreRol =x.Name}).ToList();
            _response.Resultado=roles;
            _response.IsExitoso=true;
            _response.StatusCode=System.Net.HttpStatusCode.OK;

            return Ok(_response);    
        }
    }
}

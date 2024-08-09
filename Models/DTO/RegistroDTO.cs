using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class RegistroDTO
    {
         
        public int Id { get; set; }
        [Required (ErrorMessage ="El nombre de usuario es requerido.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "El password de usuario es requerido.")]
        [StringLength(10,MinimumLength =6,ErrorMessage ="El password debe ser minimo de 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Los apellidos de usuario son requeridos.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Los nombres de usuario son requeridos.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El email de usuario es requerido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El rol de usuario son requeridos.")]
        public string Rol { get; set; }
    }
}

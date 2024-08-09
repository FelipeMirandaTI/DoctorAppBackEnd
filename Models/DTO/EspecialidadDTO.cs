using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class EspecialidadDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "El nombre de la especialidad debe ser minimo de 5 caracteres y maximo de 60.")]
        public string NombreEspecialidad { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "El nombre de la especialidad debe ser minimo de 5 caracteres y maximo de 60.")]
        public string Descripcion { get; set; }

        public int Estado { get; set; }

    }
}

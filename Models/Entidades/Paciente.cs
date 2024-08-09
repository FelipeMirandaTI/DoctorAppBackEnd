using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Apellidos son requeridos")]
        [StringLength(60,MinimumLength =1,ErrorMessage ="Apellidos debe ser minimo 1 maximo 60 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Nombres son requeridos")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombres debe ser minimo 1 maximo 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Direccion son requeridos")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Direccion debe ser minimo 1 maximo 60 caracteres")]
        public string Direccion { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Telefono debe ser minimo 1 maximo 60 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Genero son requeridos")]
        public char Genero { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public HistoriaClinica historiaClinica { get; set; }

    }
}

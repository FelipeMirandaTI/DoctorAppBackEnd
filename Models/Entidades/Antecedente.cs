using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Antecedente
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Historia clinica es requerida")]
        public Guid HistoriaClinicaId { get; set; }

        [ForeignKey("HistoriaClinicaId")]
        public HistoriaClinica historiaClinica { get; set; }

        [Required(ErrorMessage = "Observacion clinica es requerida")]
        public string Observacion { get; set; }

        public  DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}

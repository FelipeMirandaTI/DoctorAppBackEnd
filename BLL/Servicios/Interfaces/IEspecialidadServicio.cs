using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    public interface IEspecialidadServicio
    {
        Task<IEnumerable<EspecialidadDTO>> ObtenerTodos();
        Task<IEnumerable<EspecialidadDTO>> ObtenerActivos();

        Task<EspecialidadDTO> Agregar(EspecialidadDTO especialidadDTO);
        Task Actualizar(EspecialidadDTO especialidadDTO);
        Task Remover(int id);
    }
}

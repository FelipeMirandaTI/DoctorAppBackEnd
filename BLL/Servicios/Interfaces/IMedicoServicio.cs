using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    public interface IMedicoServicio
    {
        Task<IEnumerable<MedicoDTO>> ObtenerTodos();
        Task<MedicoDTO> Agregar(MedicoDTO MedicoDTO);
        Task Actualizar(MedicoDTO MedicoDTO);
        Task Remover(int id);
    }
}

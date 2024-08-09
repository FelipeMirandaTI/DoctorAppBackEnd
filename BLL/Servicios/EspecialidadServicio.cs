using AutoMapper;
using BLL.Servicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTO;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios
{
    public class EspecialidadServicio : IEspecialidadServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public EspecialidadServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        public async Task Actualizar(EspecialidadDTO especialidadDTO)
        {
            try 
            {
                var especialidadDb = await _unidadTrabajo.Especialidad.ObtenerPrimero(e => e.Id == especialidadDTO.Id);
                if (especialidadDb == null)
                    throw new TaskCanceledException("La especialidad no existe");
                especialidadDb.NombreEspecialidad=especialidadDTO.NombreEspecialidad;
                especialidadDb.Descripcion = especialidadDTO.Descripcion;
                especialidadDb.Estado = especialidadDTO.Estado == 1 ? true : false;
                _unidadTrabajo.Especialidad.Actualizar(especialidadDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<EspecialidadDTO> Agregar(EspecialidadDTO especialidadDTO)
        {
            try 
            {
                Especialidad especialidad = new Especialidad
                {
                    NombreEspecialidad = especialidadDTO.NombreEspecialidad,
                    Descripcion = especialidadDTO.Descripcion,
                    Estado = especialidadDTO.Estado == 1 ? true : false,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                };
                await _unidadTrabajo.Especialidad.Agregar(especialidad);
                await _unidadTrabajo.Guardar();
                if (especialidad.Id == 0)
                    throw new TaskCanceledException("La especialidad no se pudo crear");
                return _mapper.Map<EspecialidadDTO>(especialidad);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<IEnumerable<EspecialidadDTO>> ObtenerTodos()
        {
            try 
            {
                var lista = await _unidadTrabajo.Especialidad.ObtenerTodos(
                                    orderBy: e => e.OrderBy(e => e.NombreEspecialidad));
                return _mapper.Map<IEnumerable<EspecialidadDTO>>(lista);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Remover(int id)
        {
            try
            {
                var especialidadDb = await _unidadTrabajo.Especialidad.ObtenerPrimero(e => e.Id == id);
                if (especialidadDb == null)
                    throw new TaskCanceledException("La especialidad no existe");
                _unidadTrabajo.Especialidad.Remover(especialidadDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception e) 
            {
                throw;
            }
            
        }
        public async Task<IEnumerable<EspecialidadDTO>> ObtenerActivos()
        {
            try
            {
                var lista = await _unidadTrabajo.Especialidad.ObtenerTodos( x => x.Estado==true,
                                    orderBy: e => e.OrderBy(e => e.NombreEspecialidad));
                return _mapper.Map<IEnumerable<EspecialidadDTO>>(lista);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

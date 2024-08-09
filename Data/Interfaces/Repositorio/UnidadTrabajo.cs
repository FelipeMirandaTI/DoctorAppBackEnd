using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly AppDbContext _appDbContext;
        public IEspecialidadRepositorio Especialidad { get; private set; }
        public IMedicoRepositorio Medico { get; private set; }

        public UnidadTrabajo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Especialidad = new EspecialidadRepositorio(_appDbContext);
            Medico= new MedicoRepositorio(_appDbContext);
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public async Task Guardar()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}

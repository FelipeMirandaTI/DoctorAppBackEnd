﻿using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.Repositorio
{
    public class EspecialidadRepositorio : Repositorio<Especialidad>, IEspecialidadRepositorio
    {
        private readonly AppDbContext _db;

        public EspecialidadRepositorio(AppDbContext db):base(db) 
        {
            _db = db;
        }

        public void Actualizar(Especialidad especialidad)
        {
            var especialidadDb = _db.Especialidad.FirstOrDefault( e => e.Id == especialidad.Id );
            if ( especialidadDb != null )
            {
                especialidadDb.NombreEspecialidad=especialidad.NombreEspecialidad;
                especialidadDb.Descripcion =especialidad.Descripcion;
                especialidadDb.Estado =especialidad.Estado;
                especialidadDb.FechaActualizacion =DateTime.Now;
                _db.SaveChanges();
            }
        }
    }
}

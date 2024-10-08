﻿using AutoMapper;
using Models.DTO;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Especialidad, EspecialidadDTO>()
                     .ForMember(d => d.Estado,m=> m.MapFrom(o => o.Estado ==true ? 1:0));

            CreateMap<Medico, MedicoDTO>()
                 .ForMember(d => d.Estado, m => m.MapFrom(o => o.Estado == true ? 1 : 0))
                 .ForMember(d => d.NombreEspecialidad, m => m.MapFrom(o => o.Especialidad.NombreEspecialidad));
        }
    }
}

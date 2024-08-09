using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : IdentityDbContext<UsuarioAplicacion,RolAplicacion,int,IdentityUserClaim<int>
        ,RolUsuarioAplicacion,IdentityUserLogin<int>,IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UsuarioAplicacion> usuarioAplicacion { get; set; }
        public DbSet <Usuario> Usuarios { get; set; }
        // para agregar la migracion se debe ejecutar el siguiente comando en la consola de paquetes nugets,
        // debe apuntar al proyecto Data
        // add-migration MigracionInicial
        public DbSet<Especialidad> Especialidad { get; set;}
        //por cada entidad va haber un archivo de configuracion
        public DbSet<Medico> Medicos { get; set; }

        public DbSet<Paciente> pacientes { get; set; }

        public DbSet<HistoriaClinica> historiasClinicas { get; set; }
        public DbSet<Antecedente> antecedentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

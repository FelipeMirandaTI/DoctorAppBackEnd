using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Inicializador
{
    public class DbInicializador : IdbInicializador
    {
        private readonly AppDbContext _appContext;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly RoleManager<RolAplicacion> _roleManager;

        public DbInicializador(AppDbContext appContext, UserManager<UsuarioAplicacion> userManager, RoleManager<RolAplicacion> roleManager)
        {
            _appContext = appContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Inicializar()
        {
            try
            {
                if (_appContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _appContext.Database.Migrate(); //Cuando se ejecuta por primera vez la app y hay migraciones pendientes
                }
            }
            catch (Exception)
            {

                throw;
            }
            //Datos iniciales
            //Crear roles
            if (_appContext.Roles.Any(r => r.Name == "Admin")) return;

            _roleManager.CreateAsync(new RolAplicacion { Name = "Admin" }).GetAwaiter().GetResult(); ;
            _roleManager.CreateAsync(new RolAplicacion { Name = "Agendador" }).GetAwaiter().GetResult(); ;
            _roleManager.CreateAsync(new RolAplicacion { Name = "Doctor" }).GetAwaiter().GetResult(); ;

            //Crear usuario Administrador
            var usuario = new UsuarioAplicacion
            {
                UserName="administrador",
                Email="administrador@doctorapp.cl",
                Apellidos="Miranda",
                Nombres="Felipe"
            };
            _userManager.CreateAsync(usuario,"Admin123").GetAwaiter().GetResult(); ;
            UsuarioAplicacion usuarioAplicacion = await _appContext.usuarioAplicacion.Where(u => u.UserName == "administrador").FirstOrDefaultAsync();
            _userManager.AddToRoleAsync(usuarioAplicacion,"Admin").GetAwaiter().GetResult();

        }
    }
}

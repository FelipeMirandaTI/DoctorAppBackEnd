using API.Extensiones;
using API.Middleware;
using Data.Inicializador;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AgregarServiciosAplicacion(builder.Configuration); //agregar el metodo que contiene los servicios
builder.Services.AgregarServiciosIdentidad(builder.Configuration);
builder.Services.AddScoped<IdbInicializador,DbInicializador>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errores/{0}");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors( x => x.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod());
app.UseAuthentication();

app.UseAuthorization();

using (var scoped = app.Services.CreateScope())
{
    var services=scoped.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var inicializador = services.GetRequiredService<IdbInicializador>();
        inicializador.Inicializar();
    }
    catch (Exception e)
    {

        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Ocurrio un error al ejecutar la migracion");
    }
}

    app.MapControllers();

app.Run();

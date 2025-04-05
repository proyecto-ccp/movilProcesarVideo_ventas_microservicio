using Videos.Aplicacion.Comandos;
using Videos.Dominio.Puertos.Repositorios;
using Videos.Infraestructura.Adaptadores.Repositorios;
using Videos.Infraestructura.Adaptadores.RepositorioGenerico;
using Microsoft.EntityFrameworkCore;;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<VideosDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("VideosDbContext")), ServiceLifetime.Transient);
builder.Services.AddTransient(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
builder.Services.AddTransient<IVideoRepositorio, VideoRepositorio>();
builder.Services.AddScoped<IComandosVideo, ManejadorComandos>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Aplicacao;
using Aplicacao.Commands.CriarPedido;
using Aplicacao.Commands.DeletePedido;
using Aplicacao.Commands.UpdatePedido;
using Aplicacao.Queries.GetAllItems;
using Aplicacao.Queries.GetAllPedidos;
using Aplicacao.Queries.GetPedidoById;
using Dominio.Interfaces;
using FluentValidation;
using Infra.Contexto;
using Infra.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGoodHamburgerContext, GoodHamburgerContext>();

builder.Services.AddDbContext<GoodHamburgerContext>(options =>
{
    options.UseNpgsql("Host=postgres;Port=5432;Database=postgres;Username=postgres;Password=postgres"); //docker

    //options.UseNpgsql("Host=localhost;Port=5434;Database=postgres;Username=postgres;Password=postgres"); //local
});

// Registrar todos os handlers do MediatR automaticamente da assembly Aplicacao
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(UpdatePedidoCommand).Assembly);
});

// Registrar todos os validators automaticamente da assembly Aplicacao
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(typeof(UpdatePedidoCommand).Assembly);

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();

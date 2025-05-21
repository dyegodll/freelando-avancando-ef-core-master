﻿using Freelando.Api.Converters;
using Freelando.Api.Requests;
using Freelando.Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freelando.Api.Endpoints;

public static class ClienteExtension
{
    public static void AddEndPointClientes(this WebApplication app)
    {
        app.MapGet("/clientes", async ([FromServices] ClienteConverter converter, [FromServices] FreelandoContext contexto) =>
        {
            var clientes = converter.EntityListToResponseList(contexto.Clientes.ToList());

            return Results.Ok(await Task.FromResult(clientes));
        }).WithTags("Cliente").WithOpenApi();

        app.MapGet("/clientes/por-email", async ([FromServices] FreelandoContext contexto, string email) =>
        {
            var clientes = contexto.Clientes.Where(c => c.Email!.Equals(email)).ToList();

            return Results.Ok(await Task.FromResult(clientes));
        }).WithTags("Cliente").WithOpenApi();

        app.MapGet("/clientes/identificador-nome", async ( [FromServices] FreelandoContext contexto) =>
        {
            var clientes = contexto.Clientes.Select(c => new { Identificador = c.Id, Nome = c.Nome });

            return Results.Ok(await Task.FromResult(clientes));
        }).WithTags("Cliente").WithOpenApi();

        //Include: uma forma de usar SELECT com JOIN, se for relacionar mais de uma tabela acrescentar o ThenInclude
        app.MapGet("/clientes/projeto-especialidade", async ( [FromServices] FreelandoContext contexto) =>
        {
            var clientes = contexto.Clientes.Include( p => p.Projetos ).ThenInclude( e => e.Especialidades ).AsSplitQuery().ToList(); //AsSplitQuery() quebra a consulta única complexa com diversos INNER JOINs e SELECTs em consultas separadas e menores, memlhorando significativamente a PERFORMANCE de execução.

            return Results.Ok(await Task.FromResult(clientes));
        }).WithTags("Cliente").WithOpenApi();

        app.MapPost("/cliente", async ([FromServices] ClienteConverter converter, [FromServices] FreelandoContext contexto, ClienteRequest clienteRequest) =>
        {
            var cliente = converter.RequestToEntity(clienteRequest);

            await contexto.Clientes.AddAsync(cliente);
            await contexto.SaveChangesAsync();

            return Results.Created($"/cliente/{cliente.Id}", cliente);
        }).WithTags("Cliente").WithOpenApi();

        app.MapPut("/cliente/{id}", async ([FromServices] ClienteConverter converter, [FromServices] FreelandoContext contexto, Guid id, ClienteRequest clienteRequest) =>
        {
            var cliente = await contexto.Clientes.FindAsync(id);
            if (cliente is null)
            {
                return Results.NotFound();
            }
            var clienteAtualizado = converter.RequestToEntity(clienteRequest);
            cliente.Nome = clienteAtualizado.Nome;
            cliente.Cpf = clienteAtualizado.Cpf;
            cliente.Email = clienteAtualizado.Email;
            cliente.Telefone = clienteAtualizado.Telefone;

            await contexto.SaveChangesAsync();

            return Results.Ok((cliente));
        }).WithTags("Cliente").WithOpenApi();

        app.MapDelete("/cliente/{id}", async ([FromServices] ClienteConverter converter, [FromServices] FreelandoContext contexto, Guid id) =>
        {
            var cliente = await contexto.Clientes.FindAsync(id);
            if (cliente is null)
            {
                return Results.NotFound();
            }

            contexto.Clientes.Remove(cliente);
            await contexto.SaveChangesAsync();

            return Results.NoContent();
        }).WithTags("Cliente").WithOpenApi();

    }
}

using ApiColegioPagos.Data;
using ApiColegioPagos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ApiColegioPagosDbContext>();

// Contexto de la base de datos
builder.Services.AddDbContext<ApiColegioPagosDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("ConstStr")));


var app = builder.Build();

// Configure the HTTP request pipeline.z
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Inicializar valores por defecto
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApiColegioPagosDbContext>();
    Global global = await dbContext.Globals.FirstOrDefaultAsync(x => x.Glo_id == 1);
    Pension pension1 = await dbContext.Pensiones.FirstOrDefaultAsync(x => x.Pen_id == 1);
    Pension pension2 = await dbContext.Pensiones.FirstOrDefaultAsync(x => x.Pen_id == 2);
    Pension pension3 = await dbContext.Pensiones.FirstOrDefaultAsync(x => x.Pen_id == 3);
    Admin adminBase = await dbContext.Admins.FirstOrDefaultAsync(x => x.Id == 1);

    if (global == null)
    {
        global = new Global
        {
            Glo_nombre = "Cuota",
            Glo_valor = 0
        };
        await dbContext.Globals.AddAsync(global);
        await dbContext.SaveChangesAsync();
    }

    if (pension1 == null)
    {
        pension1 = new Pension
        {
            Pen_nombre = "Inscripción",
            Pen_valor = 0
        };

        await dbContext.Pensiones.AddAsync(pension1);
        await dbContext.SaveChangesAsync();
    }

    if (pension2 == null)
    {
        pension2 = new Pension
        {
            Pen_nombre = "Normal",
            Pen_valor = 100
        };

        await dbContext.Pensiones.AddAsync(pension2);
        await dbContext.SaveChangesAsync();
    }

    if( adminBase == null)
    {
        adminBase = new Admin
        {
            contrasenia = "1q2w3e4r"
        };
        await dbContext.Admins.AddAsync(adminBase);
        await dbContext.SaveChangesAsync();
    }
}

app.Run();

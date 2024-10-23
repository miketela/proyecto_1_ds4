using System.IO;
using Microsoft.EntityFrameworkCore;
using Calculadora.Models;

namespace Calculadora.Data;

public class CalculadoraContext : DbContext
{
    public DbSet<Calculo> Calculos { get; set; }

    public CalculadoraContext()
    {
        // Crear la base de datos si no existe
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Ruta de la base de datos SQLite
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "calculadora.db3");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
}


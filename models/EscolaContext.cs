using Microsoft.EntityFrameworkCore;
using ProjetoEscola_API.Models;
using System.Diagnostics.CodeAnalysis;

namespace ProjetoEscola_API.Data
{
  public class EscolaContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        
        public EscolaContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("StringConexaoSQLServer"));
        }
        public DbSet<Aluno>? Aluno { get; set; }
        public DbSet<Nota>? Nota { get; set; }
        public DbSet<Professor>? Professor { get; set; }
        public DbSet<Disciplina>? Disciplina { get; set; }
        public DbSet<CursoEscola>? CursoEscola {get;set;}
    }
}
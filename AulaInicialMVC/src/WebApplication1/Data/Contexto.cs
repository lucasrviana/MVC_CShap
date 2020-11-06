using DevOI.UI.WebModelo.Models;
using Microsoft.EntityFrameworkCore;

namespace DevOI.UI.WebModelo.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions options) 
            : base(options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
    }
}

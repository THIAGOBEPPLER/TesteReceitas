using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteReceitas.Entities;

namespace TesteReceitas.Data
{
    public class ReceitaContext : DbContext
    {
        public DbSet<Receita> Receitas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=1234;Persist Security Info=True;User ID=sa;Initial Catalog=TesteReceita;Data Source=DESKTOP-2EBCKJD\\SQLEXPRESS");
        }
    }
}

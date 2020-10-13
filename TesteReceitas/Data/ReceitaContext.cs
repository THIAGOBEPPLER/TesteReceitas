﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<ReceitaIngrediente> ReceitasIngredientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=1234;Persist Security Info=True;User ID=sa;Initial Catalog=TesteReceita;Data Source=DESKTOP-2EBCKJD\\SQLEXPRESS");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReceitaIngrediente>().HasKey(ri => new { ri.ReceitaId, ri.IngredienteId });
        }
    }
}

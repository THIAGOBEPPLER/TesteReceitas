using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TesteReceitas.Entities
{
    public class Receita
    {
        [Key]
        public int Id  { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public int Duracao { get; set; }
        // public List<Ingrediente> Ingredientes { get; set; }

    }
}

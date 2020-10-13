using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TesteReceitas.Entities
{
    public class ReceitaIngrediente
    {

        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }

        public int IngredienteId { get; set; }
        public Ingrediente Ingrediente { get; set; }
        

    }
}

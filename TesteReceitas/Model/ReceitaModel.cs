using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteReceitas.Model
{
    public class ReceitaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public int Duracao { get; set; }
        public int[] Ingredientes { get; set; }
    }
}

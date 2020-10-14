using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteReceitas.Data;
using TesteReceitas.Entities;
using TesteReceitas.Model;

namespace TesteReceitas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {
        ReceitaContext bd = new ReceitaContext();
        [HttpGet()]
        public List<RetornaIngredientesModel> BuscaIngrediente()
        {
            var query =
                (from i in bd.Ingredientes
                 select new RetornaIngredientesModel {
                     Id = i.Id,
                     Nome = i.Nome
                 }).ToList();

            return query;
        }
        [HttpGet("{sujestao}")]
        public dynamic Sugestao()
        {
            string ingredientes = Request.Query["ingredientes"];

            if (ingredientes == null)
                return "Lista vazia.";

            var arrayIngredientes = ingredientes.Split(',');

            List<int> listaIngredientes = new List<int>();

            foreach (var i in arrayIngredientes)
            {
                listaIngredientes.Add(int.Parse(i));
            }

                return (arrayIngredientes);
        
        }
    }
}

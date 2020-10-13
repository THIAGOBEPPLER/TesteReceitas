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
    [Route("api/receita")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        ReceitaContext bd = new ReceitaContext();

        [HttpPost("{nome}")]
        public ActionResult<string> Adiciona([FromBody] NovaReceitaModel request)
        {
            var receita = new Receita();

            receita.Nome = request.Nome;
            receita.Categoria = request.Categoria;
            receita.Descricao = request.Descricao;
            receita.Duracao = request.Duracao;
            receita.Ingredientes = request.Ingredientes;

            bd.Receitas.Add(receita);
            bd.SaveChanges();

            return Ok("Cadastrado");
        }

        [HttpPut("{id}")]
        public ActionResult<string> Edita([FromBody] ReceitaModel request)
        {
            var id = request.Id;

            var query =
               (from r in bd.Receitas
                where r.Id == id
                select r).SingleOrDefault();

            if (query == null)
                return BadRequest("Nao cadastrado.");

            var receita = new Receita();

            receita.Nome = request.Nome;
            receita.Categoria = request.Categoria;
            receita.Descricao = request.Descricao;
            receita.Duracao = request.Duracao;
            receita.Ingredientes = request.Ingredientes;

            bd.Receitas.Update(receita);
            bd.SaveChanges();

            return Ok("Cadastrado");
        }
        [HttpGet("{tipo}")]
        public ActionResult<ReceitaModel[]> Busca([FromBody] BuscaReceitaModel request)
        {

            var tipo = request.Tipo;
            var pesquisa = request.Pesquisa;

            if (tipo == "Categoria")
            {

                var query =
                   (from r in bd.Receitas
                    where r.Categoria == pesquisa
                    select new ReceitaModel{ 
                        Id = r.Id, 
                        Nome = r.Nome, 
                        Categoria = r.Categoria, 
                        Descricao = r.Descricao, 
                        Duracao = r.Duracao, 
                        Ingredientes = r.Ingredientes 
                    }).ToList();


                return Ok(query);
            }
                
            else if (tipo == "Nome")
            {
                var query =
                   (from r in bd.Receitas
                    where  r.Categoria.Contains(pesquisa) 
                    select new ReceitaModel { 
                        Id = r.Id, 
                        Nome = r.Nome, 
                        Categoria = r.Categoria, 
                        Descricao = r.Descricao, 
                        Duracao = r.Duracao, 
                        Ingredientes = r.Ingredientes 
                    }).ToList();



                return Ok(query);
            }

            else
                return BadRequest("Tipo nao encontrado");
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Deleta(int id)
        {
            var query =
                  (from r in bd.Receitas
                   where r.Id == id
                   select r).SingleOrDefault();

            if (query == null)
                return BadRequest("Receita nao encontrada");

            var receita = query;

            bd.Receitas.Remove(receita);
            bd.SaveChanges();


            return Ok("Receita deletada");
        }
    }
}

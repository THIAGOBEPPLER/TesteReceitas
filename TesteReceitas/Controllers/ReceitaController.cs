﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Internal;
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
        public string Adiciona([FromBody] NovaReceitaModel request)
        {
            var receita = new Receita();
            int[] ingredientes = request.Ingredientes;

            receita.Nome = request.Nome;
            receita.Categoria = request.Categoria;
            receita.Descricao = request.Descricao;
            receita.Duracao = request.Duracao;
            ingredientes = request.Ingredientes;

            bd.Receitas.Add(receita);
            bd.SaveChanges();

            // receita.Id;

            var ri = new ReceitaIngrediente();
            ri.ReceitaId = receita.Id;
            foreach (int i in ingredientes)
            {
                ri.IngredienteId = i;
                bd.ReceitasIngredientes.Add(ri);
                bd.SaveChanges();
            }
            

            return ("Cadastrado.");
        }

        [HttpPut("{id}")]
        public string Edita([FromBody] ReceitaModel request)
        {
            var id = request.Id;

            var query =
               (from r in bd.Receitas
                where r.Id == id
                select r).SingleOrDefault();

            if (query == null)
                return ("Receita nao encontrada.");

            var receita = query;
            int[] ingredientes = request.Ingredientes;



            receita.Nome = request.Nome;
            receita.Categoria = request.Categoria;
            receita.Descricao = request.Descricao;
            receita.Duracao = request.Duracao;

            bd.Receitas.Update(receita);
            bd.SaveChanges();


            var limpa = (from r in bd.ReceitasIngredientes
                     where r.ReceitaId == id
                     select r);

            bd.ReceitasIngredientes.RemoveRange(limpa);
            bd.SaveChanges();

            var ri = new ReceitaIngrediente();
            ri.ReceitaId = receita.Id;
            foreach (int i in ingredientes)
            {
                ri.IngredienteId = i;
                bd.ReceitasIngredientes.Add(ri);
                bd.SaveChanges();
            }




            return ("Editado.");
        }
        [HttpGet()]
        public ActionResult<ReceitaModel[]> Busca(BuscaReceitaModel request)
        {
            string nome = Request.Query["nome"];
            string categoria = Request.Query["categoria"];

            // var lista = new List<in>;

            if (nome == "" && categoria == "")
            {
                var query =
               (from r in bd.Receitas
                join ri in bd.ReceitasIngredientes on r.Id equals ri.ReceitaId
                where r.Id == ri.ReceitaId
                select new 
                {
                    Id = r.Id,
                    Nome = r.Nome,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Duracao = r.Duracao,
                    Ingredientes = ri.IngredienteId
                }).ToList() ;

        

                return Ok(query);
            }

            else if (categoria == "")
            {
                var query =
               (from r in bd.Receitas
                join ri in bd.ReceitasIngredientes on r.Id equals ri.ReceitaId
                where r.Nome.Contains(nome)
                select new
                {
                    Id = r.Id,
                    Nome = r.Nome,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Duracao = r.Duracao,
                    Ingredientes = ri.IngredienteId
                }).ToList();



                return Ok(query);
            }
            else if (nome == "")
            {
                var query =
               (from r in bd.Receitas
                join ri in bd.ReceitasIngredientes on r.Id equals ri.ReceitaId
                where r.Categoria == categoria
                select new
                {
                    Id = r.Id,
                    Nome = r.Nome,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Duracao = r.Duracao,
                    Ingredientes = ri.IngredienteId
                }).ToList();



                return Ok(query);
            }
            else
            {
                var query =
               (from r in bd.Receitas
                join ri in bd.ReceitasIngredientes on r.Id equals ri.ReceitaId
                where r.Categoria == categoria && r.Nome.Contains(nome)
                select new
                {
                    Id = r.Id,
                    Nome = r.Nome,
                    Categoria = r.Categoria,
                    Descricao = r.Descricao,
                    Duracao = r.Duracao,
                    Ingredientes = ri.IngredienteId
                }).ToList();



                return Ok(query);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<string> Deleta(int id)
        {
            var query =
                  (from r in bd.Receitas
                   where r.Id == id
                   select r).SingleOrDefault();

            if (query == null)
                return BadRequest("Receita nao encontrada.");

            var receita = query;

            bd.Receitas.Remove(receita);
            bd.SaveChanges();


            return Ok("Receita deletada.");
        }

        [HttpGet("{id}")]
        public ActionResult<string> BuscaIngredientes(int id)
        {
            var query =
               (from r in bd.Receitas
                where r.Id == id
                select r).SingleOrDefault();

            if (query == null)
                return BadRequest("Receita nao encontrada.");


            var query2 =
               (from r in bd.Receitas
                where r.Id == id
                join ri in bd.ReceitasIngredientes on r.Id equals ri.ReceitaId
                select ri.IngredienteId).ToArray();

            return Ok(query2);
        }
    }
}

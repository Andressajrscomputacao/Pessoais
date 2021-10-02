using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCalculaPreco.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCalculaPreco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastrarController : Controller
    {
        private readonly ApiContext ctx;
        public CadastrarController(ApiContext ctx)
        {
            this.ctx = ctx;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ativo");
        }


        [HttpGet("{descricao}/{preco}/{categoria}")]
        public IActionResult Get(string descricao, double preco, string categoria)
        {
            if (string.IsNullOrEmpty(descricao))
                return Unauthorized("O preenchimento da descrição é obrigatório");
            if (string.IsNullOrEmpty(categoria))
                return Unauthorized("O preenchimento da categoria é obrigatório");
            if (preco <= 0)
                return Unauthorized("O preenchimento do preço de custo é obrigatório");
            if (descricao.Length > 50 )
                return Unauthorized("O tamanho máximo da descrião é de 50 caracteres.");

            try
            {

                Produto produto = new Produto();

                produto.ID = ctx.Produtos.LastOrDefault().ID + 1;
                produto.Descricao = descricao;
                produto.Categoria = (descricao.ToLower().Contains("softplan")) ? "Softplan" : categoria;
                produto.PrecoCusto = preco;

                double perc = obterpercentual(produto.Categoria);
                var vlpercentual = ((double)perc / 100) * produto.PrecoCusto;
                produto.PrecoVenda = preco + vlpercentual;
                produto.DataCriacao = DateTime.Now;

                ctx.Produtos.Add(produto);
                ctx.SaveChanges();

                return Ok("Produto cadastrado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private double obterpercentual(string categoria)
        {
            double porcentagem = 0;
            switch (categoria.ToUpper())
            {
                case "BRINQUEDOS":
                    porcentagem = 25;
                    break;
                case "BEBIDAS":
                    porcentagem = 30;
                    break;
                case "INFORMÁTICA":
                    porcentagem = 10;
                    break;
                case "SOFTPLAN":
                    porcentagem = 5;
                    break;
                default:
                    porcentagem = 15;
                    break;
            }

            return porcentagem;
        }
    }
}

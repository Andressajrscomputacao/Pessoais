using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCalculaPreco.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiCalculaPreco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculaPrecoController : Controller
    {
        private readonly ApiContext ctx;
        public CalculaPrecoController(ApiContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ativo");
        }

        [HttpGet("{categoria}/{preco}")]
        public async Task<IActionResult> GetAsync(string categoria, double preco)
        {
            if (string.IsNullOrEmpty(categoria))
                return Unauthorized("Categoria inválida");

            try
            {
                List<Produto> produtos = await ctx.Produtos.ToListAsync();

                Produto produto = new Produto();
                List<Produto> lista = await produto.calcularAsync(categoria, preco, produtos);
                var json = JsonConvert.SerializeObject(lista);
                return Ok(json);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        


    }
}

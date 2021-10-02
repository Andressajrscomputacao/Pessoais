using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCalculaPreco.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiCalculaPreco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultarController : Controller
    {
        private readonly ApiContext ctx;
        public ConsultarController(ApiContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                List<Produto> produtos = await ctx.Produtos.ToListAsync();
                var json = JsonConvert.SerializeObject(produtos.OrderBy(x => x.Descricao));
                return Ok(json);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

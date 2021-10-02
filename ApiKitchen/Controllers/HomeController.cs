using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kitchen.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly DBContext ctx;
        public HomeController(DBContext ctx)
        {
            this.ctx = ctx;
        }

        /// <summary>
        /// Initial Method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ativo");
        }

        /// <summary>
        /// Receive Order to Line Order
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [HttpGet("{productID}")]
        public async Task<IActionResult> GetAsync(int productID)
        {
            if (productID <= 0)
                return Unauthorized("Unknow Product.");

            try
            {
                Product product = await ctx.Products
                        .Include(x => x.Kitchen)                
                        .Where(x => x.ID == productID).FirstOrDefaultAsync();

                Line line = new Line();
                line.ID = Guid.NewGuid().ToString();
                line.ProductID = product.ID;
                line.KitchenID = product.KitchenID;
                ctx.Lines.Add(line);
                ctx.SaveChanges();

                return Ok($"Order: Product: {product.Name} in Kitchen: {product.Kitchen.Title} successfully!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

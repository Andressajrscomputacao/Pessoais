using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCalculaPreco.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApiCalculaPreco
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());
            services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("RazorPagesApp"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var context = serviceProvider.GetService<ApiContext>();
            AdicionarDadosTeste(context);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AdicionarDadosTeste(ApiContext context)
        {
            List<Produto> produtos = new List<Produto>();
            produtos.Add(new Produto(1, "Boneca Barbie", "Brinquedos"));
            produtos.Add(new Produto(2, "Boneca Baby Alive", "Brinquedos"));
            produtos.Add(new Produto(3, "Água", "Bebidas"));
            produtos.Add(new Produto(4, "Suco", "Bebidas"));
            produtos.Add(new Produto(5, "Teclado", "Informática"));
            produtos.Add(new Produto(6, "Mouse", "Informática"));
            produtos.Add(new Produto(7, "Software", "Softplan"));
            produtos.Add(new Produto(8, "Café", "Toda e qualquer outra categoria informada"));

            foreach (var item in produtos)
            {
                context.Produtos.Add(item);
            }

            context.SaveChanges();
        }


    }
}

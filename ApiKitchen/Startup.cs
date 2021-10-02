using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kitchen.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kitchen
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
            services.AddDbContext<DBContext>(options => options.UseInMemoryDatabase("RazorPagesApp"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var context = serviceProvider.GetService<DBContext>();
            AdicionarDadosTeste(context);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AdicionarDadosTeste(DBContext context)
        {
            //mock product
            List<Product> products = new List<Product>();
            products.Add(new Product(1, "Fires", 20.00, 1));
            products.Add(new Product(2, "Grill", 24.00, 1));
            products.Add(new Product(3, "Salad", 7.50, 1));
            products.Add(new Product(4, "Drink", 15.50, 2));
            products.Add(new Product(5, "Desert", 19.50, 2));

            foreach (var item in products)
            {
                context.Products.Add(item);
            }


            //mock kitchen
            List<AreaKitchen> kitchens = new List<AreaKitchen>();
            kitchens.Add(new AreaKitchen(1, "First Area"));
            kitchens.Add(new AreaKitchen(2, "Second Area"));

            foreach (var item in kitchens)
            {
                context.Kitchens.Add(item);


            }
            context.SaveChanges();
        }


    }
}

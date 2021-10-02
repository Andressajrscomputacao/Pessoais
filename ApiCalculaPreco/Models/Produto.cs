using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCalculaPreco.Models
{
    public class Produto
    {
        public Produto()
        {

        }

        public Produto(int iD, string descricao, string categoria)
        {
            ID = iD;
            Descricao = descricao;
            Categoria = categoria;
        }

        public int ID { get; set; }
        public string Descricao { get; set; }
        public double PrecoVenda { get; set; }
        public double PrecoCusto { get; set; }
        public string Categoria { get; set; }
        public DateTime DataCriacao { get; set; }


        public async Task<List<Produto>> calcularAsync(string categoria, double preco, List<Produto> produtos)
        {
            List<Produto> lista = produtos.Where(x => x.Categoria.ToUpper().Equals(categoria.ToUpper())).ToList();
            double perc = obterpercentual(categoria);
            foreach (var item in lista)
            {
                var vlpercentual = ((double)perc / 100) * preco;
                item.PrecoVenda = preco + vlpercentual;
            }

            return lista;
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

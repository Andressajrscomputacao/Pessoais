using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public class AreaKitchen
    {
        public AreaKitchen(int iD, string title)
        {
            ID = iD;
            Title = title;
            Products = new List<Product>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

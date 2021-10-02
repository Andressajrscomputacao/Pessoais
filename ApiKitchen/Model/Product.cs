using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public class Product
    {
        public Product(int iD, string name, double price, int kitchenID)
        {
            ID = iD;
            Name = name;
            Price = price;
            KitchenID = kitchenID;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int KitchenID { get; set; }
        public AreaKitchen Kitchen { get; set; }
    }
}

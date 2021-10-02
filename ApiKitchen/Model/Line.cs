using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Model
{
    public class Line
    {
        public string ID { get; set; }
        public int ProductID { get; set; }
        public DateTime DtFinished  { get; set; }
        public int KitchenID { get; set; }

    }
}

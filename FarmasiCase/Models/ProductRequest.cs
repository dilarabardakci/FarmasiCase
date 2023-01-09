using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmasiCase.Models
{
    public class ProductRequest
    {
        public string name { get; set; }
        public double price { get; set; }

        public ProductRequest(string name, double price)
        {
            this.name = name;
            this.price = price;

        }

    }
}

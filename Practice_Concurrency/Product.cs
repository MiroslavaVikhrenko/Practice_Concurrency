using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Concurrency
{
    public class Product
    {
        public int Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ConcurrencyCheck]
        public int Quantity { get; set; }
    }
}

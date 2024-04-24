using System;
using System.Collections.Generic;

namespace SecurityServices.Models
{
    public partial class Product
    {
        public Product()
        {
            Producttransactionhistories = new HashSet<Producttransactionhistory>();
        }

        public string Id { get; set; } = null!;
        public int Code { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Isavailable { get; set; }

        public virtual ICollection<Producttransactionhistory> Producttransactionhistories { get; set; }
    }
}

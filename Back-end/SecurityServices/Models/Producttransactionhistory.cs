using System;
using System.Collections.Generic;

namespace SecurityServices.Models
{
    public partial class Producttransactionhistory
    {
        public string Id { get; set; } = null!;
        public string Transactionid { get; set; } = null!;
        public string? Productname { get; set; }
        public string Productid { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
        public virtual Transaction Transaction { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace SecurityServices.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            Producttransactionhistories = new HashSet<Producttransactionhistory>();
        }

        public string Id { get; set; } = null!;
        public string Userid { get; set; } = null!;
        public DateTime Datetime { get; set; }
        public decimal Amount { get; set; }
        public string? Transactionstatus { get; set; }
        public string? Clientname { get; set; }
        public string? Companyemail { get; set; }
        public string? Scope { get; set; }
        public string? Limitation { get; set; }
        public string? Authdetails { get; set; }
        public string? Schedule { get; set; }
        public string? Otherservices { get; set; }
        public string Transactionid { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Producttransactionhistory> Producttransactionhistories { get; set; }
    }
}

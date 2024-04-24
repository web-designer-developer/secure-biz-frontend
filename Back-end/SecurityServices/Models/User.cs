using System;
using System.Collections.Generic;

namespace SecurityServices.Models
{
    public partial class User
    {
        public User()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string? Lastname { get; set; }
        public string Hash { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public int Failedloginattempts { get; set; }
        public DateTime? Lastfailedattempt { get; set; }
        public int Emailverified { get; set; }
        public string? Lastotp { get; set; }
        public DateTime? Lastotpsent { get; set; }
        public string? Companyname { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

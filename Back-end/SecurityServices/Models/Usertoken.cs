using System;
using System.Collections.Generic;

namespace SecurityServices.Models
{
    public partial class Usertoken
    {
        public string Id { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Sessionid { get; set; } = null!;
        public string? Userid { get; set; }
        public DateTime Generatedat { get; set; }
        public DateTime Expiresat { get; set; }
    }
}

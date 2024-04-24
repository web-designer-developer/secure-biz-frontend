using System;
using System.Collections.Generic;

namespace SecurityServices.Models
{
    public partial class Usercontact
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Company { get; set; }
        public string? Message { get; set; }
    }
}

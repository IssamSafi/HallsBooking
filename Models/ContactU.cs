using System;
using System.Collections.Generic;

#nullable disable

namespace Firstproject.Models
{
    public partial class ContactU
    {
        public decimal ConId { get; set; }
        public string Location { get; set; }
        public decimal? Telephone { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string YEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Firstproject.Models
{
    public partial class Testimonial
    {
        public decimal Id { get; set; }
        public string Tname { get; set; }
        public decimal? Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Approved { get; set; }
    }
}

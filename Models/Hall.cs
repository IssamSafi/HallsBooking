using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Firstproject.Models
{
    public partial class Hall
    {
        public Hall()
        {
            Userhalls = new HashSet<Userhall>();
        }

        public decimal HallId { get; set; }
        public string HallName { get; set; }
        public int? HallPrice { get; set; }
        public string HallSize { get; set; }
        public string HallType { get; set; }
        public decimal? AddressId { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Userhall> Userhalls { get; set; }
    }
}

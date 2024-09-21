using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Firstproject.Models
{
    public partial class User
    {
        public User()
        {
            Logins = new HashSet<Login>();
            Userhalls = new HashSet<Userhall>();
        }

        public decimal UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public string Imagepath { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Userhall> Userhalls { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Firstproject.Models
{
    public partial class Login
    {
        public decimal LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal? UserId { get; set; }
        public decimal? RoleId { get; set; }

        public virtual Role1 Role { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Firstproject.Models
{
    public partial class Role1
    {
        public Role1()
        {
            Logins = new HashSet<Login>();
        }

        public decimal RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}

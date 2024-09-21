using System;
using System.Collections.Generic;

#nullable disable

namespace Firstproject.Models
{
    public partial class Userhall
    {
        public decimal Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? UserId { get; set; }
        public decimal? HallId { get; set; }
        public bool? IsApprove { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual User User { get; set; }
    }
}

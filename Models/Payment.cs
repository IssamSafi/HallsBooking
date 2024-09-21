using System;
using System.Collections.Generic;

#nullable disable

namespace Firstproject.Models
{
    public partial class Payment
    {
        public decimal PaymentId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? UserId { get; set; }
        public decimal? Balance { get; set; }
        public string CardNumber { get; set; }
    }
}

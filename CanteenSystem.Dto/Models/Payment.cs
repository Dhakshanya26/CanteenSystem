using System;
using System.Collections.Generic;

#nullable disable

namespace CanteenSystem.Dto.Models
{
    public partial class Payment
    {
        public long Id { get; set; }
        public string PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public long OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace CanteenSystem.Dto.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
        }

        public long Id { get; set; }
        public string OrderReference { get; set; }
        public int UserProfileId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace CanteenSystem.Dto.Models
{
    public partial class OrderItem
    {
        public long Id { get; set; }
        public int MealMenuId { get; set; }
        public DateTime MealMenuOrderDate { get; set; }
        public long OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public virtual MealMenu MealMenu { get; set; }
        public virtual Order Order { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace CanteenSystem.Dto.Models
{
    public partial class MealMenu
    {
        public MealMenu()
        {
            Carts = new HashSet<Cart>();
            MealMenuAvailabilities = new HashSet<MealMenuAvailability>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string MealName { get; set; }
        public int MealTypeId { get; set; }
        public double Price { get; set; }
        public int? DiscountId { get; set; }
        public string ImageName { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual MealType MealType { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<MealMenuAvailability> MealMenuAvailabilities { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}

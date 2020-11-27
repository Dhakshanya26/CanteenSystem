using System;
using System.Collections.Generic;

#nullable disable

namespace CanteenSystem.Web.Models
{
    public partial class MealMenuAvailability
    {
        public int Id { get; set; }
        public int MealMenuId { get; set; }
        public DateTime AvailabilityDate { get; set; }
        public int Quantity { get; set; }
        public virtual MealMenu MealMenu { get; set; }
    }
}

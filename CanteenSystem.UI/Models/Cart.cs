﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CanteenSystem.UI.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int MealMenuId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public decimal CreatedDate { get; set; }
        public decimal UpdatedDate { get; set; }
        public DateTime MealAvailableDate { get; set; }

        public virtual MealMenu MealMenu { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace CanteenSystem.Dto.Models
{
    public partial class Discount
    {
        public Discount()
        {
            MealMenus = new HashSet<MealMenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime ValidFromDate { get; set; }
        public DateTime ValidToDate { get; set; }

        public virtual ICollection<MealMenu> MealMenus { get; set; }
    }
}

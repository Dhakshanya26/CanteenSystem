using System;
using System.Collections.Generic;

#nullable disable

namespace CanteenSystem.Dto.Models
{
    public partial class MealType
    {
        public MealType()
        {
            MealMenus = new HashSet<MealMenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MealMenu> MealMenus { get; set; }
    }
}

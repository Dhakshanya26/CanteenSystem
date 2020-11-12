//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CanteenSystem.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class MealMenu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MealMenu()
        {
            this.Carts = new HashSet<Cart>();
            this.MealMenuAvailabilities = new HashSet<MealMenuAvailability>();
            this.OrderItems = new HashSet<OrderItem>();
        }
    
        public int Id { get; set; }
        public string MealName { get; set; }
        public int MealTypeId { get; set; }
        public double Price { get; set; }
        public int DiscountId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual Discount Discount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MealMenuAvailability> MealMenuAvailabilities { get; set; }
        public virtual MealType MealType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}

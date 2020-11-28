using System.Collections.Generic;

namespace CanteenSystem.Dto.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int RollNumber { get; set; }
        public string Department { get; set; }
        public bool IsVerified { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<ParentMapping> StudentUserProfiles { get; set; }
        public virtual ICollection<ParentMapping> ParentUserProfiles { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }

    }


    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int RollNumber { get; set; }
        public string Department { get; set; }
        public bool IsVerified { get; set; }
        public string ApplicationUserId { get; set; }
        public string Role { get; set; }
    }
}

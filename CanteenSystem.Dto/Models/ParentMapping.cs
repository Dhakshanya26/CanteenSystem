using System.Collections.Generic;

namespace CanteenSystem.Dto.Models
{
    public class ParentMapping
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ParentId { get; set; }
        public virtual UserProfile StudentUserProfile { get; set; }
        public virtual UserProfile ParentUserProfile { get; set; }
    }
}

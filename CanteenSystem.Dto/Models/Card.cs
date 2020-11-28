using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanteenSystem.Dto.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public decimal AvailableBalance { get; set; }
        public bool IsActive { get; set; }
        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}

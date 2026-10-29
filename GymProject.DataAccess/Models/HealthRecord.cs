using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Models
{
    public class HealthRecord:BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string? Note { get; set; }
        public string BloodType { get; set; } = null!;
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
    }
}

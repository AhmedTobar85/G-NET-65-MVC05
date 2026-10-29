using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Models
{
    public class MemberShip:BaseEntity
    {
      public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
        //public DateOnly StartDate { get; set; }=CreatedAt
        public DateTime EndDate { get; set; }

        public bool IsActive => EndDate > DateTime.Now;

        public string Status => IsActive ? "Active" : "Expired";





    }
}

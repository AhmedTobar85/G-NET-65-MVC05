using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Models
{
    public class Booking:BaseEntity
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
    
        public bool IsActive { get; set; }
    }
}

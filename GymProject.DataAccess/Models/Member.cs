using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Models
{
    public class Member:User
    {
        public string? Photo { get; set; }
        //Joindate=>CreatedAt in BaseEntity
        public HealthRecord HealthRecord { get; set; } = null!;
        public ICollection<MemberShip> Memberships { get; set; } = [];
        public ICollection<Booking> MemberSessions { get; set; } = [];



    }
}

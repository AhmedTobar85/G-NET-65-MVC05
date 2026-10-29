using GymProject.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Models
{
    public class Trainer:User
    {
        public Specialty Specialty { get; set; }
        public ICollection<Session> Sessions { get; set; } = [];
    }
}

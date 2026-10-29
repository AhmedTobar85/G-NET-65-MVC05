using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Session> Sessions { get; set; } = [];
    }
}

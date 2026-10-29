using GymProject.DataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.DataAccess.Models
{
    public class User:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }

        public Gender Gender { get; set; }
        public Address Address { get; set; }
    }
    [Owned]
    public class Address
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public int BuildingNumber { get; set; }
    }
}

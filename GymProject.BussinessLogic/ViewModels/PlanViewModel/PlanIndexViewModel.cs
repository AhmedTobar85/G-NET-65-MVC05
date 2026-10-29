using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.BussinessLogic.ViewModels.PlanViewModel
{
    public class PlanIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationDays  { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

    }
}

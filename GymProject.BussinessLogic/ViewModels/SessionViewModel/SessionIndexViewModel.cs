using System;
using System.Collections.Generic;
using System.Text;

namespace GymProject.BussinessLogic.ViewModels.SessionViewModel
{
    public partial class SessionIndexViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Trainer { get; set; } = default!;
        public string Category { get; set; } = default!;

        public int AvailableSlots { get; set; }

        public string Date => $"{StartDate:MMM dd,yyyy}";
        public string TimeRangeDisplay => $"{StartDate:hh:mm tt} - {EndDate:hh:mm tt}";
        public TimeSpan Duration => EndDate - StartDate;

        public string Status
        {
           get
            {
                if (StartDate > DateTime.Now)
                    return "Upcoming";
                else if (StartDate <= DateTime.Now && EndDate >= DateTime.Now)
                    return "Ongoing";
                else
                    return "Completed";

           }
        }
    }
}

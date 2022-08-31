using System;

namespace HouseCleaningThuyGiangNguyen
{
    internal class Appointment
    {
        public bool IsNew { get; set; } = true;
        public int? AppointmentID { get; set; }
        public DateTime? ServiceDate { get; set; }
        public decimal? HouseSize { get; set; }
        public int? CustomerID { get; set; }
        public int? CleanerTypeID { get; set; }
    }
}

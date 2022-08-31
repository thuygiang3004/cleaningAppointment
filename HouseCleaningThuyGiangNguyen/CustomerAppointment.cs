using System;
using System.Collections.Generic;

namespace HouseCleaningThuyGiangNguyen
{
    internal class CustomerAppointment
    {
        public bool IsNew { get; set; } = true;
        public int? CustomerID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsNewAppointment { get; set; } = true;
        public int? AppointmentID { get; set; }
        public DateTime? ServiceDate { get; set; }
        public decimal? HouseSize { get; set; }
        public int? CleanerTypeID { get; set; }
        public CleanerType ThisCleanerType { get; set; }
        public List<CleanerType> CleanerTypeList { get; set; }
        public decimal? Cost { get; set; }
    }
}

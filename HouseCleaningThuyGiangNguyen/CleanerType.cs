using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseCleaningThuyGiangNguyen
{
    internal class CleanerType
    {
            public int? ID { get; set; }
            public string Description { get; set; } = string.Empty;
            public decimal? RatePerMeter { get; set; }
    }
}

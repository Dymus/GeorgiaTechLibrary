using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public string PostCode { get; set; } = null!;
        public string? Street { get; set; }
        public string? StreetNum { get; set; }
    }
}

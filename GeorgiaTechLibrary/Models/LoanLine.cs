using System;
using System.Collections.Generic;
namespace GeorgiaTechLibrary.Models
{
    public partial class LoanLine
    {
        public int LoanId { get; set; }
        public int VolumeId { get; set; }
        public bool IsReturned { get; set; }
        public byte[] EndDateTime { get; set; } = null!;

        public Volume Volume { get; set; } = null!;
        public Notice? Notice { get; set; }
    }
}

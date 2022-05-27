using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public partial class Notice
    {
        public int LoanId { get; set; }
        public int VolumeId { get; set; }
        public string Message { get; set; } = null!;
        public byte[] IssueDate { get; set; } = null!;
    }
}

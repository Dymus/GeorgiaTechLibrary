using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public partial class Notice
    {
        public int LoanId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime IssueDate { get; set; }
    }
}

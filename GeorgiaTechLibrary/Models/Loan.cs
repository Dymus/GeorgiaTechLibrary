using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeorgiaTechLibrary.Models
{
    public partial class Loan
    {
        public int LoanId { get; set; }
        public byte[] StartDateTime { get; set; } = null!;
        public ICollection<LoanLine>? LoanLines { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeorgiaTechLibrary.Models
{
    public partial class Loan
    {
        public int LoanId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; } 
        public Volume volume { get; set; } 
    }
}

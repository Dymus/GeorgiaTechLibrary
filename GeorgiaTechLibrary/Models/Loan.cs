using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeorgiaTechLibrary.Models
{
    public class Loan
    {
        public int Loan_id { get; set; }
        public DateTime Start_date_time { get; set; }
        public DateTime End_date_time { get; set; } 
        public bool Is_returned { get; set; }
        public Volume Volume { get; set; } 

        public Member Member { get; set; }

    }

    public class LoanDTO
    {
        public string volume_id { get; set; }
        public string SSN { get; set; }
    }
}

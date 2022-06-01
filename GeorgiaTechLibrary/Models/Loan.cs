using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeorgiaTechLibrary.Models
{
    public class Loan
    {
        public int Loan_id { get; set; }
        public DateTime Start_data_time { get; set; }
        public DateTime End_date_time { get; set; } 
        public Volume Volume { get; set; } 

        public Member Member { get; set; }

    }

    public class LoanDTO
    {
        public int Loan_id { get; set; }
        public DateTime Start_data_time { get; set; }
        public DateTime End_date_time { get; set; }
        public int Volume_id { get; set; }
        public int SSN { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public partial class MemberType
    {
        public int MemberTypeId { get; set; }
        public string Type { get; set; } = null!;
        public int LoanPeriod { get; set; }
        public int GracePeriod { get; set; }

    }
}

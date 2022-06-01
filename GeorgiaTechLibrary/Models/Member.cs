using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public class Member
    {
        public string SSN { get; set; } = null!;
        public Location Campus_location { get; set; } = null!;
        public Location Home_location { get; set; } = null!;
        public Library Library { get; set; } = null!;
    }

    public class MemberDTO
    {
        public string SSN { get; set; } = null!;
        public int Campus_location_id { get; set; }
        public int Home_location_id { get; set; }
        public int Library_id { get; set; }

    }
}

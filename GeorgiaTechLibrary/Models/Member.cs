using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public class Member
    {
        public string SSN { get; set; } = null!;
        public Location CampusLocation { get; set; } = null!;
        public Location HomeLocation { get; set; } = null!;
        public Library Library { get; set; } = null!;
    }

    public class MemberDTO
    {
        public string SSN { get; set; } = null!;
        public int CampusLocation { get; set; }
        public int HomeLocation { get; set; }
        public int Library { get; set; }
    }
}

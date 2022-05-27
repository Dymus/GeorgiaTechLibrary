using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public class Member
    {
        public string SSN { get; set; } = null!;
        public int CampusLocation { get; set; }
        public int HomeLocation { get; set; }
        public int LibraryId { get; set; }
    }
}

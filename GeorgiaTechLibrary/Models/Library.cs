using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public class Library
    {
        public int Library_id { get; set; }
        public string Name { get; set; } = null!;
        public int Location_id { get; set; }

        public Location Location { get; set; } = null!;
        //public ICollection<Member>? Members { get; set; }
        //public ICollection<Volume>? Volumes { get; set; }
    }
}

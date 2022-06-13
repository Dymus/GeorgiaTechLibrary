using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public class Volume
    {
        public int Volume_id { get; set; }
        public string Isbn { get; set; } = null!;
        public bool Is_available { get; set; }
        public Library? Library { get; set; }

    }

    public class VolumeDTO
    {
        public string Isbn { get; set; } = null!;
        public bool Is_available { get; set; }
        public int Library_id { get; set; }
    }
}

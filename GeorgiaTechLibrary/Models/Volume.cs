using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public class Volume
    {
        public int VolumeId { get; set; }
        public string ISBN { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public Library Library { get; set; }

    }

    public class VolumeDTO
    {
        public int VolumeId { get; set; }
        public string ISBN { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public int LibraryId { get; set; }
    }
}

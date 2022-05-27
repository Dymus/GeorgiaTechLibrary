using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public partial class Volume
    {
        public int VolumeId { get; set; }
        public string ISBN { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public int LibraryId { get; set; }

    }
}

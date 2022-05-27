using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public class Card
    {
        public string SSN { get; set; } = null!;
        public byte[] ExpirationDate { get; set; } = null!;
    }
}

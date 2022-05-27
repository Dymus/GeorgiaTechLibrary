using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string FName { get; set; } = null!;

        public string LName { get; set; } = null!;
    }
}

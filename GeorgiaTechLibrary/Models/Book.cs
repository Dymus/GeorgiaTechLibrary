using System;
using System.Collections.Generic;

namespace GeorgiaTechLibrary.Models
{
    public class Book
    {
        public string ISBN { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Volume>? Volumes { get; set; }
        public ICollection<Author>? Authors { get; set; }
        public  ICollection<Subject>? Subjects { get; set; }
    }

    public class BookDTO
    {
        public string ISBN { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<VolumeDTO>? Volumes { get; set; }
    }
}

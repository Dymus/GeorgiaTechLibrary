using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeorgiaTechLibrary.Models
{
    public partial class PostCodeCity
    {
        public string PostCode { get; set; } = null!;
        public string City { get; set; } = null!;

    }
}

using System;
using System.Collections.Generic;

namespace GoReview.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
    }
}

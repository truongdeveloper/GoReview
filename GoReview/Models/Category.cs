using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoReview.Models
{
    public partial class Category
    {
        [Key]          
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }
        [Required]
        public string? Image { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}

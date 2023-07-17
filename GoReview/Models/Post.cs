
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoReview.Models
{
    public partial class Post
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int CatId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Picture { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoReview.Models
{
    public partial class Post
    {
        [Key]
        public int PostId { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User? User { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [StringLength(50)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Content { get; set; }
        public string? Picture { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime? CreateDate { get; set; }
        public int NumLike { get; set; }
        public int NumComment { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoReview.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [StringLength(500)]
        public string? CommentText { get; set; }

        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public Post? Post { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public virtual User? User { get; set; }

    }
}

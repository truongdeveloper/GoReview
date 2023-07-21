
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoReview.Models
{
    public partial class Feedback
    {
        [Key] 
        public int FeedbackId { get; set; }  

        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public virtual Post? Post {get; set; }          

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public virtual User? User { get; set; }

        public bool? Like { get; set; }
        public bool? Comment { get; set; }
    }
}

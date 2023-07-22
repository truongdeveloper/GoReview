using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoReview.Models;

public partial class Feedback
{


    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FeedbackId { get; set; }

    [ForeignKey("PostId")]
    public int PostId { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }

    public bool? Like { get; set; }

    public string? Comment { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
    

}

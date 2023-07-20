using System;
using System.Collections.Generic;

namespace GoReview.Models;

public partial class Feedback
{
    public int PostId { get; set; }

    public int FeedbackId { get; set; }

    public int UserId { get; set; }

    public bool? Like { get; set; }

    public string? Comment { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

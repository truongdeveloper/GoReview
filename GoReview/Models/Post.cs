using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoReview.Models;

public partial class Post
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PostId { get; set; }

    public int UserId { get; set; }

    public int CatId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Picture { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Category Cat { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User User { get; set; } = null!;

    // Thuộc tính mới để hiển thị dữ liệu Category
    public string? CategoryTitle => Cat?.Title;
}

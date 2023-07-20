using System;
using System.Collections.Generic;

namespace GoReview.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string ImageUser { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public string Introduce { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

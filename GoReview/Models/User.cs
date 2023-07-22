using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GoReview.Models;

public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [StringLength(80)]
    public string UserName { get; set; } = null!;

    [Required]
    public string UserEmail { get; set; } = null!;
    [Required]
    public string UserPassword { get; set; } = null!;

    public string ImageUser { get; set; } = null!;

    [Required]
    public string UserRole { get; set; } = null!;

    public string Introduce { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace GoReview.Models;

public partial class Post
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PostId { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }

    [ForeignKey("CategoryId")]
    public int CatId { get; set; }

    [StringLength(100)]
    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Picture { get; set; }

    [DataType(DataType.Date)]
    public DateTime? CreateDate { get; set; }

    public virtual Category Cat { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual User User { get; set; } = null!;

    // // Thuộc tính mới để hiển thị dữ liệu Category
    
    public string? CategoryTitle => Cat?.Title;

    public int? NumberOfLikes => Feedbacks?.Count(f => f.Like == true);

    public int? NumberOfComment => Feedbacks?.Count(f => f.Comment != null);
    
    public string? UserPost => User?.UserName;

    public bool IsLiked;
    public string? UserImage => User?.ImageUser;


}

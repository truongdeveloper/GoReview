using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoReview.Models;

public partial class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Title { get; set; }

    [Required]
    public string? Image { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

using System.ComponentModel.DataAnnotations;

namespace GoReview.Models
{
    public class User
    {
            [Key]
            public int UserID { get; set; }
            [Required]
            public string? FullName { get; set; }
            [Required]
            public string? UserName { get; set; }
            [Required]
            public string? UserEmail { get; set; }
            [Required]
            public string? UserPassword { get; set; }
            [Required]
            public string? ImageUser { get; set; }
            [Required]
            public string? UserRole { get; set; }
            [Required]
            public string? Introduce { get; set;}
    }
}

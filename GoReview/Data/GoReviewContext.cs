using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoReview.Models;

namespace GoReview.Data
{
    public class GoReviewContext : DbContext
    {
        public GoReviewContext (DbContextOptions<GoReviewContext> options)
            : base(options)
        {
        }

        public DbSet<GoReview.Models.User> User { get; set; } = default!;

        public DbSet<GoReview.Models.Post>? Post { get; set; }

        public DbSet<GoReview.Models.Feedback>? Feedback { get; set; }

        public DbSet<GoReview.Models.Comment>? Comment { get; set; }

        public DbSet<GoReview.Models.Category>? Category { get; set; }
    }
}

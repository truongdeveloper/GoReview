using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GoReview.Models;

public partial class BtlG21Context : DbContext
{
    public BtlG21Context()
    {
    }

    public BtlG21Context(DbContextOptions<BtlG21Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    
}

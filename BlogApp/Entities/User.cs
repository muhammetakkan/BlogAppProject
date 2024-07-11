using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Entities;

public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string? UserName { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? Password { get; set; }

    [StringLength(255)]
    public string? Image { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("User")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

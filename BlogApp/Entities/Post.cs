using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Entities;

public partial class Post
{
    [Key]
    public int PostId { get; set; }

    [StringLength(255)]
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Content { get; set; }

    [StringLength(255)]
    public string? Url { get; set; }

    [StringLength(255)]
    public string? Image { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PublishedOn { get; set; }

    public bool IsActive { get; set; }

    public int UserId { get; set; }

    [InverseProperty("Post")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [ForeignKey("UserId")]
    [InverseProperty("Posts")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("PostId")]
    [InverseProperty("Post")]
    public virtual ICollection<Tag> Tag { get; set; } = new List<Tag>();
}

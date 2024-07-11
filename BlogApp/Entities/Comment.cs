using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Entities;

public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    public string? Text { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PublishedOn { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    [ForeignKey("PostId")]
    [InverseProperty("Comments")]
    public virtual Post Post { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual User User { get; set; } = null!;
}

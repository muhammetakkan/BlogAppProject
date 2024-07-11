using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Entities;

public partial class Tag
{
    [Key]
    public int TagId { get; set; }

    [StringLength(100)]
    public string? Text { get; set; }

    [StringLength(255)]
    public string? Url { get; set; }

    [ForeignKey("TagId")]
    [InverseProperty("Tag")]
    public virtual ICollection<Post> Post { get; set; } = new List<Post>();
}

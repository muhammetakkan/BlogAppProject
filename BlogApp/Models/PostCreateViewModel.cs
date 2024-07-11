using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class PostCreateViewModel
    {
        public int PostId { get; set; }

        [MaxLength(100)]
        [MinLength(5)]
        [Required]
        public string? Title { get; set; }

        [MaxLength(100)]
        [MinLength(5)]
        [Required]
        public string? Description { get; set; }

        [MaxLength(100)]
        [MinLength(5)]
        [Required]
        public string? Url { get; set; }

        [MaxLength(100)]
        [MinLength(5)]
        [Required]
        public string? Content { get; set; }

        public bool isActive { get; set; }

    }
}

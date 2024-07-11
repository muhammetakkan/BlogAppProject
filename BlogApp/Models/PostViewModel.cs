using BlogApp.Entities;

namespace BlogApp.Models
{
    public class PostViewModel
    {
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();
    }
}

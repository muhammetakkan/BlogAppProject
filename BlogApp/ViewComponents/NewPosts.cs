using BlogApp.Dal.Abstract;
using BlogApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class NewPosts : ViewComponent
    {
        private readonly IPostRepository<Post> _postRepository;
        public NewPosts(IPostRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var newPosts = await _postRepository.Posts.Take(1).ToListAsync();
            return View(newPosts);
        }
    }
}

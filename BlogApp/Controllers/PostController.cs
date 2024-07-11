//using BlogApp.Models;
using BlogApp.Dal.Abstract;
using BlogApp.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository<Post> _postRepository;

        public PostController(IPostRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
            var post = _postRepository.Posts;
            var model =  new PostViewModel
            {
                Posts = await post.ToListAsync(),
            };
            return View(model);
        }

        public async Task<IActionResult> Details(string? url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            var post = await _postRepository.Posts
                .Include(i => i.User)
                .Include(i => i.Tag)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .FirstOrDefaultAsync(i => i.Url == url);
            if (post is not null)
            {
                return View(post);
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            if (model is null)
                return NotFound();
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _postRepository.Create(
                    new Post
                    {
                        Title = model.Title,
                        Content = model.Content,
                        Url = model.Url,
                        UserId = int.Parse(userId ?? ""),
                        PublishedOn = DateTime.Now,
                        Image = "1.jpg",
                        IsActive = false
                    }
                );
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> List()
        {
            var posts = await _postRepository.Posts.ToListAsync().ConfigureAwait(false);
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}

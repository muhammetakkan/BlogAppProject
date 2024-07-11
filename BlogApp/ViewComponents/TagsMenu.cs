using BlogApp.Dal.Abstract;
using BlogApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class TagsMenu : ViewComponent
    {
        private readonly ITagRepository<Tag> _tagRepository;
        public TagsMenu(ITagRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = await _tagRepository.Tags.ToListAsync();
            return View(tags);
        }

    }
}

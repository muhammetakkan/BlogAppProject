using BlogApp.Dal.Abstract;
using BlogApp.Dal.Concrete;
using BlogApp.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository<User> _userRepository;
        public UserController(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        //!!!
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Post");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUser = await _userRepository.Users.FirstOrDefaultAsync(i => i.Email == model.Email && i.Password == model.Password);
                if(isUser is not null)
                {
                    var userClaims = new List<Claim>(); //claims bir genericList oluşturduk. Giriş bilgilerimiz bir listenin içinde tutacağız.

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));

                    if(isUser.Email is "emin@gmail.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    //almış oldugumuz bilgileri 
                    var cookieClaimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var isPersistent = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(cookieClaimsIdentity),isPersistent);
                    //Giriş yaparken HttpContext ile giriş şartları veya kuralları olarak düşünülebilir.

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("","Hata");
                }
            }

            return View(model);
        }

        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.Users.FirstOrDefaultAsync(i => i.UserName == model.UserName || i.Email == model.Email);
                if(user is null)
                {
                    user = new User
                    {
                        UserName = model.UserName,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Image = "avatar.jpg"
                    };
                    await _userRepository.Create(user);
                    
                    return RedirectToAction("Index", "Post");
                }
            }
            return View(model);
        }
    
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "User");
        }

        public async Task<IActionResult> ProfileDetails(int? id)
        {
            if (id is null)
                return NotFound();

            var user = await _userRepository.Users
                .Include(i=> i.Posts)
                .Include(i=> i.Comments)
                .ThenInclude(i=> i.Post)
                .FirstOrDefaultAsync(i=> i.UserId == id);

            if (user is null)
                return NotFound();

            return View(user);
        }
    }
}

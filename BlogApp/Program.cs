using BlogApp.Dal.Abstract;
using BlogApp.Dal.Concrete;
using BlogApp.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPostRepository<Post>, EfPostRepository<Post>>();
builder.Services.AddScoped<IUserRepository<User>, EfUserRepository<User>>();
builder.Services.AddScoped<ITagRepository<Tag>, EfTagRepository<Tag>>();
builder.Services.AddScoped<ICommentRepository<Comment>, EfCommentRepository<Comment>>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => { options.LoginPath = "/User/Login"; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "post_details", //route ın adı
    pattern: "post/details/{url}", //çalışma yapısı
    defaults: new { controller = "Post", action = "Details" } //tetikleme düzeneği
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Post}/{action=Index}");

app.Run();

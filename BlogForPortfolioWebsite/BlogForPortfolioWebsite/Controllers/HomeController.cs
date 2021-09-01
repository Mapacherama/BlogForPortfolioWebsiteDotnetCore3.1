using System;
using BlogForPortfolioWebsite.Data.FileManager;
using BlogForPortfolioWebsite.Data.Repository;
using BlogForPortfolioWebsite.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BlogForPortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repo;
        private readonly IFileManager _fileManager;

        public HomeController(IRepository repo,
            IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
            var comment = new Maincomment();
        }

        public IActionResult Index(string category) =>
            View(string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category));

        [HttpGet]
        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            if (post.Image == "Error")
            {
                post.Image = String.Empty;
            }

            return View(post);
        }

        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult Image(string image) => new FileStreamResult(_fileManager.ImageStream(image),
            $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

        /*public IActionResult Index(string category)
        {
            var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category);
            // boolean ? true : false; 1 = 1? run : ignore.
            return View(posts);
        }*/

        /*[HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }*/
    }
}
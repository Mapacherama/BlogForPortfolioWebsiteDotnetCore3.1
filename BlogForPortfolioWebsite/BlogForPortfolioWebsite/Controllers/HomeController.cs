using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogForPortfolioWebsite.Data.FileManager;
using BlogForPortfolioWebsite.Data.Repository;
using BlogForPortfolioWebsite.Models.Comments;
using BlogForPortfolioWebsite.ViewModels;
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
        
        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return Post(vm.PostId);

            var post = _repo.GetPost(vm.PostId);
            if (vm.MainCommentId > 0)
            {
                post.MainComments ??= new List<MainComment>();
                
                post.MainComments.Add(new MainComment
                {
                    Message  = vm.Message,
                    Created = DateTime.Now
                });
                
                _repo.UpdatePost(post);
            }
            else
            {
                var comnew = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now
                };
            }

            await _repo.SaveChangesAsync();

            return View();
        }

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
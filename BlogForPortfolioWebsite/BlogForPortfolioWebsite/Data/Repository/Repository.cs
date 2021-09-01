using BlogForPortfolioWebsite.Models;
using BlogForPortfolioWebsite.Models.Comments;
using BlogForPortfolioWebsite.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlogForPortfolioWebsite.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _ctx;

        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<Post> GetAllPosts(string category)
        {
            // inCategory(a) = 5
            // inCategory(b) = 10
            // var a = 5
            // F#, clojure, Haskell.
            
            return _ctx.Posts
                .Where(post => post.Category.ToLower().Equals(category.ToLower()))
                .ToList();
        }

        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts
                .Include(p => p.MainComments)
                .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }
        
        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public void AddSubComment(SubComment comment)
        {
            _ctx.SubComments.Add(comment);
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
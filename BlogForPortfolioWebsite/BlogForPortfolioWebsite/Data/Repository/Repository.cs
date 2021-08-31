using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogForPortfolioWebsite.Models;
using Microsoft.EntityFrameworkCore;

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
            return _ctx.Posts.FirstOrDefault(p => p.Id == id);
        }
        
        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
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
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogForPortfolioWebsite.Models;

namespace BlogForPortfolioWebsite.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        List<Post> GetAllPosts(string category);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);

        Task<bool> SaveChangesAsync();
    }
}
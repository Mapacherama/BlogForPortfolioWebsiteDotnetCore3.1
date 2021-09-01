using System.Collections.Generic;
using System.Threading.Tasks;
using BlogForPortfolioWebsite.Models;
using BlogForPortfolioWebsite.Models.Comments;

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
        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsync();
    }
}
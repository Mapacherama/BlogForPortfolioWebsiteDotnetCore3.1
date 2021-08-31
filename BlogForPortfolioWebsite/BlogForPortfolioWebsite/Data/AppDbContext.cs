using BlogForPortfolioWebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogForPortfolioWebsite.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public DbSet<Post> Posts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
        {
            
        }

       
    }
}
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlogForPortfolioWebsite.Data.FileManager
{
    public interface IFileManager
    {
        FileStream ImageStream(string image);
        Task<string> SaveImage(IFormFile image);
        void RemoveImage(string image);
    }
}
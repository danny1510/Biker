using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Biker.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string carpeta);
        bool DeleteImageAsync(string path);
    }
}
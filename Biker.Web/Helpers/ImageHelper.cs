using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Biker.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {

        public async Task<string> UploadImageAsync(IFormFile imageFile, string carpeta)
        {
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";

            var path = Path.Combine(
             Directory.GetCurrentDirectory(),
             $"wwwroot\\images\\{carpeta}",
             file);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);

            }
            return $"~/images/{carpeta}/{file}";
        }


        public bool DeleteImageAsync(string path)
        {

            try
            {
                File.Delete($"{"wwwroot/"}path");
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }






    }
}

using BAS.AppCommon;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment appEnvironment;
        private readonly string[] enableExtensions = new string[]
        {
            ".jpeg",
            ".jpg",
            ".png"
        };

        public FileService(IHostingEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }

        private string GenerateMoviePosterName(long movieId, IFormFile file)
        {
            var extension = "." + file.FileName.Split(".").Last();
            string fileName = "Movie_" + movieId;
            string path = this.appEnvironment.WebRootPath + "\\MovieImages\\";

            if (File.Exists(path + fileName + extension))
            {
                long i = 1;
                var fileFound = true;

                do
                {
                    var tempFileName = fileName + "_" + i;

                    if (!File.Exists(path + tempFileName + extension))
                    {
                        fileFound = false;
                        fileName = tempFileName;
                    }

                    i++;
                } while (fileFound);
            }

            return $"{fileName}{extension}";
        }

        public async Task<string> InsertMoviePoster(long movieId, IFormFile file)
        {
            if (file == null)
                return "";

            var extension = "." + file.FileName.Split(".").Last();

            if (file.Length <= 0 ||
                file.Length > StaticValues.MoviePosterMaxFileSize ||
                 !enableExtensions.Any(e => e == extension))
                return "";

            var fileName = GenerateMoviePosterName(movieId, file);
            string path = this.appEnvironment.WebRootPath + "\\MovieImages\\";

            using (var stream = File.Create(path + fileName))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<string> UpdateMoviePoster(long movieId, string posterName, IFormFile file)
        {
            DeleteMoviePoster(posterName);
            return await InsertMoviePoster(movieId, file);
        }

        public void DeleteMoviePoster(string posterName)
        {
            if (posterName == "")
                return;

            string path = this.appEnvironment.WebRootPath + "\\MovieImages\\";

            if (File.Exists(path + posterName))
            {
                File.Delete(path + posterName);
            }
        }

        public FileDTO GetMoviePoster(string posterName)
        {
            string path = this.appEnvironment.WebRootPath + "\\MovieImages\\" + posterName;

            if (!File.Exists(path))
                return null;

            var image = Convert.ToBase64String(File.ReadAllBytes(path));
            var extension = Path.GetExtension(path);
            var contentType = "";
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
            }

            return new FileDTO()
            {
                File = image,
                ContentType = contentType
            };
        }
    }
}

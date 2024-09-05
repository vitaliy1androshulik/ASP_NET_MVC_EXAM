using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FileService : IFileService
    {
        const string imageFolder = "images";
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public void DeleteProductImage(string path)
        {
            string root = environment.WebRootPath;
            string fullPath = environment.WebRootPath + path;

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public async Task<string> EditProductImage(string oldPath, IFormFile newFile)
        {
            DeleteProductImage(oldPath);
            return await SaveProductImage(newFile);
        }

        public async Task<string> SaveProductImage(IFormFile file)
        {
            // get image destination path
            string root = environment.WebRootPath;      // wwwroot
            string name = Guid.NewGuid().ToString();    // random name
            string extension = Path.GetExtension(file.FileName); // get original extension
            string fullName = name + extension;         // full name: name.ext

            // create destination image file path
            string imagePath = Path.Combine(imageFolder, fullName);
            string imageFullPath = Path.Combine(root, imagePath);

            // save image on the folder
            using (FileStream fs = new FileStream(imageFullPath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            // return image file path
            return Path.DirectorySeparatorChar + imagePath;
        }
    }
}

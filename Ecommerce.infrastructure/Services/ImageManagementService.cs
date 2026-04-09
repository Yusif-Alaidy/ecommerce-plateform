using Ecommerce.core.Service_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.infrastructure.Services
{
    public class ImageManagementService : IImageManagementService
    {
        #region Constructore & Fields
        private readonly IFileProvider fileProvider;
        public ImageManagementService(IFileProvider fileProvider = null)
        {
            this.fileProvider = fileProvider;
        }
        #endregion

        #region Add Image
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            List<string> SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", src);

            if (Directory.Exists(ImageDirectory) is not true)
            {
                Directory.CreateDirectory(ImageDirectory);
            }
            foreach (var image in files)
            {
                if (image.Length > 0)
                {
                    var ImageName = image.Name;
                    var ImageSrc = $"/Images/{src}/{ImageName}";

                    var root = Path.Combine(ImageDirectory, ImageName);

                    using (FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(ImageSrc);
                }
            }
            return SaveImageSrc;
        }
        #endregion

        #region Delete Image
        public void DeleteImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;

            File.Delete(root);
        }
        #endregion
    }
}

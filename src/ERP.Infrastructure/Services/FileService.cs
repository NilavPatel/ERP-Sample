using ERP.Domain.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ERP.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _config;
        public FileService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<byte[]> DownloadFile(string name)
        {
            var target = _config.GetValue<string>("UploadFolderPath");
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new Exception("File Upload Path Is Not Set");
            }

            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
            if (!File.Exists(Path.Join(target, name)))
            {
                throw new Exception("Document Not Found.");
            }
            return await System.IO.File.ReadAllBytesAsync(Path.Join(target, name));
        }

        public async Task UploadFile(IFormFile file, Guid id)
        {
            if (file.Length <= 0)
            {
                return;
            }

            var target = _config.GetValue<string>("UploadFolderPath");
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new Exception("File Upload Path Is Not Set");
            }

            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
            string ext = Path.GetExtension(file.FileName);
            var filePath = Path.Combine(target, id + ext);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
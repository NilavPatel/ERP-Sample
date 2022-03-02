using Microsoft.AspNetCore.Http;

namespace ERP.Application.Core.Services
{
    public interface IFileService
    {
        public Task UploadFile(IFormFile file, Guid id);

        public Task<byte[]> DownloadFile(string name);
    }
}
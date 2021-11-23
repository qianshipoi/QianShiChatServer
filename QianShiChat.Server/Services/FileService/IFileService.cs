using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace QianShiChat.Server.Services.FileService
{
    public interface IFileService
    {
        Task<bool> SaveFileAsync(string fileName, IFormFile file, CancellationToken cancellationToken = default);

        Task<bool> SaveFileAsync(string fileName, Stream fileStream, CancellationToken cancellationToken = default);

        Task<string> GetFileUrlAsync(string fileName);
        string GetFileUrl(string fileName);

    }
}

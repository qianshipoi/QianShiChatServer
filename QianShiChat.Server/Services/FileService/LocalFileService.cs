using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Furion.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using QianShiChat.Server.Configs;

namespace QianShiChat.Server.Services.FileService
{
    public class LocalFileService : IFileService, ITransient
    {
        private readonly string _wwwrootPath;
        private readonly ProjectConfig _projectConfig;
        public LocalFileService(IWebHostEnvironment webHostEnvironment,
            IOptionsMonitor<ProjectConfig> projectConfigOption)
        {
            _wwwrootPath = webHostEnvironment.WebRootPath;
            _projectConfig = projectConfigOption.CurrentValue;
        }

        public string GetFileUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            var filePath = Path.Combine(_wwwrootPath, fileName.Trim('\\'));
            return File.Exists(filePath) ?  $"{_projectConfig.ProjectUrl}/{fileName.Trim('\\').Replace("\\", "/")}" : string.Empty;
        }

        public async Task<string> GetFileUrlAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            var filePath = Path.Combine(_wwwrootPath, fileName.Trim('\\'));
            if (!File.Exists(filePath))
            {
                return await Task.FromResult(string.Empty);
            }
            return await Task.FromResult($"{_projectConfig.ProjectUrl}/{fileName.Trim('\\').Replace("\\", "/")}");
        }

        public async Task<bool> SaveFileAsync(string fileName, IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file is null)
            {
                throw new System.ArgumentNullException(nameof(file));
            }

            var filePath = Path.Combine(_wwwrootPath, fileName.Trim('\\'));

            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
                if (dir != null)
                    Directory.CreateDirectory(dir);

            await using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            await file.CopyToAsync(fileStream, cancellationToken);
            await fileStream.FlushAsync(cancellationToken);
            return true;
        }

        public async Task<bool> SaveFileAsync(string fileName, Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream is null)
            {
                throw new System.ArgumentNullException(nameof(stream));
            }

            var filePath = Path.Combine(_wwwrootPath, fileName.Trim('\\'));

            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
                if (dir != null)
                    Directory.CreateDirectory(dir);

            await using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            await stream.CopyToAsync(fileStream, cancellationToken);
            await fileStream.FlushAsync(cancellationToken);
            return true;
        }

    }
}

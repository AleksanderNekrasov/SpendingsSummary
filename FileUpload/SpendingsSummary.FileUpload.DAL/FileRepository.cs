using Microsoft.Extensions.Options;
using SpendingsSummary.Application;
using SpendingsSummary.FileUpload.Application.Interfaces;

namespace SpendingsSummary.FileUpload.DAL
{
    public sealed class FileRepository : IFileRepository
    {
        private readonly ImportSettings _importSettings;

        public FileRepository(IOptions<ImportSettings> importSettings) =>        
            _importSettings = importSettings.Value;
        
        public async Task SaveFileAsync(string fileName, Stream stream)
        {
            string path = Path.Combine(_importSettings.ReportFilesFolder, fileName);
            using FileStream outputFileStream = new FileStream(path, FileMode.Create);
            stream.CopyTo(outputFileStream);
            await stream.FlushAsync();
        }
    }
}
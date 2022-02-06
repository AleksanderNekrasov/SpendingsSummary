namespace SpendingsSummary.FileUpload.Application.Interfaces
{
    public interface IFileRepository
    {
        Task SaveFileAsync(string fileName, Stream stream);
    }
}

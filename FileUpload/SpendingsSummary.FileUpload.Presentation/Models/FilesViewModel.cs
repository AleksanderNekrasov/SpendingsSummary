using SpendingsSummary.FileUpload.Core.Models;

namespace SpendingsSummary.FileUpload.Models
{
    public sealed record FilesViewModel(IEnumerable<FileMetadataModel>? Files);
}

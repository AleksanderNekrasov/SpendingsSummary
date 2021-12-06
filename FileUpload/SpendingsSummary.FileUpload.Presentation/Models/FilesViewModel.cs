using SpendingsSummary.FileUpload.Core.Models;

namespace SpendingsSummary.FileUpload.Models
{
    public class FilesViewModel
    {
        public FilesViewModel(IEnumerable<FileModel>? files)
        {
            Files = files;
        }

        public IEnumerable<FileModel>? Files { get; }
    }
}

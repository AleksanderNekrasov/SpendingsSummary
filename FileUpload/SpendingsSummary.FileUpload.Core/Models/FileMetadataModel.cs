namespace SpendingsSummary.FileUpload.Core.Models;

public record FileMetadataModel
{
    public FileMetadataModel(string name, long size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; }

    public long Size { get; }
}

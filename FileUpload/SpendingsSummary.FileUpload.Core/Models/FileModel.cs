namespace SpendingsSummary.FileUpload.Core.Models;

public record FileModel
{
    public FileModel(string name, long size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; }
    public long Size { get; }
}

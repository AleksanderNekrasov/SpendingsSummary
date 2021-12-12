using MediatR;

namespace SpendingsSummary.FileUpload.Application.Commands;

public class SaveFileCommand : IRequest<(bool isValid, string validationMessage)>
{
    public SaveFileCommand(string name, Stream readStream)
    {
        Name = name;
        ReadStream = readStream;
    }

    public string Name { get; }

    public Stream ReadStream { get; }
}

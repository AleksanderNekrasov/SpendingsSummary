using MediatR;
using SpendingsSummary.FileUpload.Application.Commands;

namespace SpendingsSummary.FileUpload.Application.Handlers
{
    public class SaveFileHandler : IRequestHandler<SaveFileCommand, (bool isValid, string validationMessage)>
    {
        public Task<(bool isValid, string validationMessage)> Handle(SaveFileCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

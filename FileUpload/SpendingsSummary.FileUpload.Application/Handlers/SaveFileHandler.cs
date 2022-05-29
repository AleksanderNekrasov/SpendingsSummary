using MediatR;
using SpendingsSummary.FileUpload.Application.Commands;
using SpendingsSummary.FileUpload.Application.Interfaces;
using SpendingSummary.Common.Models;
using SpendingSummary.QueueBus;

namespace SpendingsSummary.FileUpload.Application.Handlers
{
    public sealed class SaveFileHandler : IRequestHandler<SaveFileCommand, (bool isValid, string validationMessage)>
    {
        private readonly IMediator _mediator;
        private readonly IFileRepository _fileRepository;

        public SaveFileHandler(IMediator mediator, IFileRepository fileRepository)
        {
            _mediator = mediator;
            _fileRepository = fileRepository;
        }

        public async Task<(bool isValid, string validationMessage)> Handle(SaveFileCommand command, CancellationToken cancellationToken)
        {
            await _fileRepository.SaveFileAsync(command.Name, command.ReadStream);
            await _mediator.Send(new PublishEventToQueueCommand(new DataUploadedEvent { FileName = command.Name }), cancellationToken);
            return (true, string.Empty);
        }
    }
}

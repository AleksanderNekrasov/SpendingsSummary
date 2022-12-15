using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpendingsSummary.FileUpload.Core.Models;
using SpendingsSummary.FileUpload.Models;
using SpendingsSummary.FileUpload.Presentation.Converters;

namespace SpendingsSummary.FileUpload.Controllers
{
    public sealed class FileUploadController : Controller
    {
        private readonly IMediator _mediator;

        public FileUploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> IndexAsync(IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null)
            {
                return View();
            }

            using var stream = file.OpenReadStream();
            var command = file.SaveCommand(stream);
            var result = await _mediator.Send(command, cancellationToken);

            var model = new FilesViewModel(new[] { new FileMetadataModel(file.Name, file.Length) });
            return View(model);
        }
    }
}

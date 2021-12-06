using Microsoft.AspNetCore.Mvc;
using SpendingsSummary.FileUpload.Core.Models;
using SpendingsSummary.FileUpload.Models;

namespace SpendingsSummary.FileUpload.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult Index(IFormFile file)
        {
            if (file is null)
            {
                return View();
            }

            var model = new FilesViewModel(new[] { new FileModel(file.Name, file.Length) });
            return View(model);
        }
    }
}

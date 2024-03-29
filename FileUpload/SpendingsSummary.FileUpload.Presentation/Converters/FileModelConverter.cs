﻿using SpendingsSummary.FileUpload.Application.Commands;

namespace SpendingsSummary.FileUpload.Presentation.Converters
{
    internal static class FileModelConverter
    {
        internal static SaveFileCommand SaveCommand(this IFormFile file, Stream stream) 
        {
            return new SaveFileCommand(file.FileName, stream);
        }
    }
}

﻿using SpendingsSummary.FileUpload.Core.Models;

namespace SpendingsSummary.FileUpload.Models
{
    public class FilesViewModel
    {
        public FilesViewModel(IEnumerable<FileMetadataModel>? files)
        {
            Files = files;
        }

        public IEnumerable<FileMetadataModel>? Files { get; }
    }
}
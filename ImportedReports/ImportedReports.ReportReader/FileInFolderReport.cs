using SpendingsSummary.ReportReader.Interfaces;
using System.IO;

namespace SpendingsSummary.ReportReader
{
    internal sealed class FileInFolderReport : IReportPreParsed
    {
        private string _fileName;

        public FileInFolderReport(string fileName) 
        {
            _fileName = fileName; 
        }

        public TextReader GetReader() => new StreamReader(GetFile.OpenRead());

        private FileInfo GetFile => new FileInfo(_fileName);
    }
}

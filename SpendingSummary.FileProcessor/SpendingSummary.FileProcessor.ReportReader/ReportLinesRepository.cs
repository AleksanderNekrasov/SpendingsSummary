using SpendingsSummary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingsSummary.ReportReader
{
    public class ReportLinesRepository : IReportLinesRepository
    {
        private static string[] newLineSpeparators = new[] { "\r\n", "\r", "\n" };

        public async Task<IEnumerable<string>> GetLines(string fileName)
        {
            var reportInFile = new FileInFolderReport(fileName);
            using var reader = reportInFile.GetReader();
            var wholeFile = await reader.ReadToEndAsync();
            return wholeFile.Split(newLineSpeparators, StringSplitOptions.None).Skip(1);
        }
    }
}

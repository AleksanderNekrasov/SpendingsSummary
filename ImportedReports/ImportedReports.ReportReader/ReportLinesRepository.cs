using ImportedReports.Parser.ReportParser.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingsSummary.ReportReader
{
    public class ReportLinesRepository : IReportLinesRepository
    {
        private static string[] newLineSpeparators = new[] { "\r\n", "\r", "\n" };

        public async Task<string> GetHeader(string fileName)
        {
            using var reader = GetReader(fileName);
            return await reader.ReadLineAsync();
        }

        public async Task<IEnumerable<string>> GetLines(string fileName)
        {
            using var reader = GetReader(fileName);
            var wholeFile = await reader.ReadToEndAsync();
            return wholeFile.Split(newLineSpeparators, StringSplitOptions.None).Skip(1);
        }

        private TextReader GetReader(string fileName) => new FileInFolderReport(fileName).GetReader();
    }
}

using Microsoft.Extensions.Options;
using SpendingsSummary.Interfaces;
using SpendingsSummary.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SpendingsSummary.ReportReader.FilesInFolder;

namespace SpendingsSummary.Application.Interfaces
{
    public class ImportReportFromFileCase : IImportReportFromFile
    {
        private string _folderPath;
        private IReportLinesRepository _reportSourceRepo;
        private readonly ITransactionsParser _parser;

        public ImportReportFromFileCase(IOptions<ImportSettings> settings, IReportLinesRepository reportSourceRepo, ITransactionsParser parser)
        {
            _folderPath = settings.Value.ReportFilesFolder;
            _reportSourceRepo = reportSourceRepo;
            _parser = parser;
        }

        public async void ImportFileReportToDb()  
        {
            var files = GetFileNames(_folderPath);
            var tasks = files.Select(GetTransactions);
            var transactions = (await Task.WhenAll(tasks))
                .SelectMany(x => x)
                .ToArray();
        }

        private async Task<IEnumerable<TransactionModel>> GetTransactions(string file)
        {
            var lines = await _reportSourceRepo.GetLines(reportInFile);
            return await _parser.ParseTransactionFromString(lines);
        }
    }
}
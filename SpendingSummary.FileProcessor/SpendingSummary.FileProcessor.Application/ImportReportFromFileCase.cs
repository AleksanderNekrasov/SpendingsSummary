using Microsoft.Extensions.Options;
using SpendingsSummary.Interfaces;
using SpendingsSummary.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            var files = Directory.GetFiles(_folderPath);
            var tasks = files.Select(GetTransactions);
            var transactions = (await Task.WhenAll(tasks))
                .SelectMany(x => x)
                .ToArray();
        }

        private async Task<IEnumerable<TransactionModel>> GetTransactions(string file)
        {
            // Use commands Mediatr
            var lines = await _reportSourceRepo.GetLines(file);

            // Use in-memorySotrage Redis, while reading Split transactions by chunks after read is complete without issue delete from Redis,
            // if not catch error and write error status into Redis
            // When application starts, read all unprocessed events from Redis
            // When failure happens, write an appropriate status for event in Redis
            return await _parser.ParseTransactionFromString(lines);
        }
    }
}
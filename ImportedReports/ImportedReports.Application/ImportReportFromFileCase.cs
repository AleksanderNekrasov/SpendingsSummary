using ImportedReports.Model;
using ImportedReports.Parser.ReportParser.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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

        public async Task ImportFileReportToDb(CancellationToken cancellationToken)  
        {
            var tasks = Directory.GetFiles($"../{_folderPath}")
                .Select(x => GetTransactions(x, cancellationToken));
            var transactions = (await Task.WhenAll(tasks))
                .SelectMany(x => x)
                .ToArray();
            await PublishTransactionAsync(transactions, cancellationToken);
        }

        private async Task<IEnumerable<TransactionModel>> GetTransactions(string file, CancellationToken cancellationToken)
        {
            // Use commands Mediatr
            var lines = await _reportSourceRepo.GetLines(file);

            return await _parser.ParseTransactionFromString(lines, cancellationToken);
        }

        private async Task PublishTransactionAsync(IEnumerable<TransactionModel> transactions, CancellationToken cancellationToken)
        {
            //await _publisher.PublishAsync(
            //    new DataParsedEvent 
            //    { 
            //        EventId = Guid.NewGuid(),
            //        Transactions = transactions
            //    });
        }
    }
}
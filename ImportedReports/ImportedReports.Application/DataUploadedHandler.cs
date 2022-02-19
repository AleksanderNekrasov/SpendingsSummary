using ImportedReports.Model;
using ImportedReports.Parser.ReportParser.Interfaces;
using Microsoft.Extensions.Options;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace SpendingsSummary.Application
{
    public class DataUploadedHandler : IQueueEventHandler<DataUploadedEvent>
    {
        private string _folderPath;
        private IReportLinesRepository _reportSourceRepo;
        private readonly ITransactionsParser _parser;

        public DataUploadedHandler(IOptions<ImportSettings> settings, IReportLinesRepository reportSourceRepo, ITransactionsParser parser)
        {
            _folderPath = settings.Value.ReportFilesFolder;
            _reportSourceRepo = reportSourceRepo;
            _parser = parser;
        }

        public async Task HandleQueueEventAsync(DataUploadedEvent queueEvent)
        {
            var transactions = await GetTransactions(Path.Combine($"../{_folderPath}", queueEvent.FileName));
            await PublishTransactionAsync(transactions.ToArray());
        }

        private async Task<IEnumerable<TransactionModel>> GetTransactions(string file)
        {
            // Use commands Mediatr
            var lines = await _reportSourceRepo.GetLines(file);

            return _parser.ParseTransactionFromString(lines);
        }

        private async Task PublishTransactionAsync(IEnumerable<TransactionModel> transactions)
        {
            await Task.CompletedTask;
            //await _publisher.PublishAsync(
            //    new DataParsedEvent 
            //    { 
            //        EventId = Guid.NewGuid(),
            //        Transactions = transactions
            //    });
        }
    }
}
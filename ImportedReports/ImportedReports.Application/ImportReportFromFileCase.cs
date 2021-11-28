using ImportedReports.Model;
using Microsoft.Extensions.Options;
using SpendingsSummary.Interfaces;
using SpendingSummary.Common.Interfaces;
using System;
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
        private readonly IQueuePublisher _publisher;

        public ImportReportFromFileCase(IOptions<ImportSettings> settings, IReportLinesRepository reportSourceRepo, ITransactionsParser parser, IQueuePublisher publisher)
        {
            _folderPath = settings.Value.ReportFilesFolder;
            _reportSourceRepo = reportSourceRepo;
            _parser = parser;
            _publisher = publisher;
        }

        public async void ImportFileReportToDb()  
        {
            var tasks = Directory.GetFiles($"../{_folderPath}").Select(GetTransactions);
            var transactions = (await Task.WhenAll(tasks))
                .SelectMany(x => x)
                .ToArray();
            await PublishTransactionAsync(transactions);
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

        private async Task PublishTransactionAsync(IEnumerable<TransactionModel> transactions)
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
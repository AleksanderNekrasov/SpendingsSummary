using AutoMapper;
using ImportedReports.Model;
using ImportedReports.Parser.ReportParser.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using SpendingSummary.Common.Interfaces;
using SpendingSummary.Common.Models;
using SpendingSummary.QueueBus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace SpendingsSummary.Application
{
    public record EmployeeModel(int Id);

    public sealed class DataUploadedHandler : IQueueEventHandler<DataUploadedEvent>
    {
        private string _folderPath;
        private IReportLinesRepository _reportSourceRepo;
        private readonly ITransactionsParser _parser;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DataUploadedHandler(IOptions<ImportSettings> settings, IReportLinesRepository reportSourceRepo, ITransactionsParser parser, IMediator mediator, IMapper mapper)
        {
            _folderPath = settings.Value.ReportFilesFolder;
            _reportSourceRepo = reportSourceRepo;
            _parser = parser;
            _mediator = mediator;
            _mapper = mapper;
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
            await _mediator.Send(new PublishEventToQueueCommand(
                new DataParsedEvent
                {
                    EventId = Guid.NewGuid(),
                    Transactions = _mapper.Map<IEnumerable<TransactionDto>>(transactions)
                }));
        }
    }
}
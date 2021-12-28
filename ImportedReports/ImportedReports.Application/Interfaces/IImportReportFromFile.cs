using System.Threading;
using System.Threading.Tasks;

namespace SpendingsSummary.Application
{
    public interface IImportReportFromFile
    {
        Task ImportFileReportToDb(CancellationToken cancellationToken);
    }
}
